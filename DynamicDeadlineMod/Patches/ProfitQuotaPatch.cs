using HarmonyLib;
using UnityEngine;
using Anubis.LC.ExtraDays.Extensions;
using DynamicDeadlineMod.Helpers;

namespace DynamicDeadlineMod.Patches
{
    [HarmonyPatch(typeof(GameNetworkManager), nameof(GameNetworkManager.ResetSavedGameValues))]
    public class ResetSavedValuesPatch
    {
        [HarmonyPrefix]
        public static void ResetSavedValues()
        {
            string currentSaveFile = GameNetworkManager.Instance.currentSaveFileName;
            ES3.Save("previousDeadline", 3f, currentSaveFile);
            ES3.Save("totalOfAverage", 0f, currentSaveFile);
        }
    }

    [HarmonyPatch(typeof(TimeOfDay), nameof(TimeOfDay.SetNewProfitQuota))]
    public class ProfitQuotaPatch
    {
        static float quotaFulfilled;

        [HarmonyPrefix]
        public static void GetQuotaFulfilled()
        {
            quotaFulfilled = TimeOfDay.Instance.quotaFulfilled;
        }

        [HarmonyPostfix]
        static void DynamicDeadline(TimeOfDay __instance)
        {
            string currentSaveFile = GameNetworkManager.Instance.currentSaveFileName;
            bool isHost = RoundManager.Instance.NetworkManager.IsHost;
            float runCount = TimeOfDay.Instance.timesFulfilledQuota;

            float minimumDays;
            if (LethalConfigHelper.useMinMax.Value)
            {
                minimumDays = LethalConfigHelper.setMinimumDays.Value;
            }
            else
            {
                minimumDays = 3f;
            }

            float maximumDays;
            if (LethalConfigHelper.useMinMax.Value)
            {
                maximumDays = LethalConfigHelper.setMaximumDays.Value;
            }
            else
            {
                maximumDays = float.MaxValue;
            }

            float totalOfAverage;
            float previousDeadline;

            // Only included the if statements because I didn't want to attach to the lobby creation process in order to create the ES3 Keys, kinda figured it wasn't necessary to do so either.
            if (ES3.KeyExists("previousDeadline"))
            {
                previousDeadline = ES3.Load("previousDeadline", 3f);
                DynamicDeadlineModPlugin.Instance.mls.LogInfo($"Successfully loaded the previous totalOfAverage variable! totalofAverage is: {previousDeadline}");
            }
            else
            {
                ES3.Save("previousDeadline", 3f);
                previousDeadline = ES3.Load("previousDeadline", 3f);
                DynamicDeadlineModPlugin.Instance.mls.LogInfo($"Could not load previousDeadline variable as it does not exist! Creating now!");
            }

            if (ES3.KeyExists("totalOfAverage"))
            {
                totalOfAverage = ES3.Load("totalOfAverage", 0f);
                DynamicDeadlineModPlugin.Instance.mls.LogInfo($"Successfully loaded the previous totalOfAverage variable! totalofAverage is: {totalOfAverage}");
            }
            else
            {
                ES3.Save("totalOfAverage", 0f);
                totalOfAverage = ES3.Load("totalOfAverage", 0f);
                DynamicDeadlineModPlugin.Instance.mls.LogInfo($"Could not load totalOfAverage variable as it does not exist! Creating now!");
            }

            if (isHost && LethalConfigHelper.legacyCal.Value == false)
            {
                float dynamicDifficulty = Mathf.Clamp(Mathf.Ceil(quotaFulfilled / previousDeadline), LethalConfigHelper.minScrapValuePerDay.Value, 1000f);

                totalOfAverage += dynamicDifficulty;

                float quotaAverage = totalOfAverage / runCount;

                // Only included to this if statement to cover mid-run installation, so a player won't need to start a new save in order to enjoy the mod.
                if (totalOfAverage == 0 && runCount != 0)
                {
                    totalOfAverage = LethalConfigHelper.minScrapValuePerDay.Value * runCount;
                    quotaAverage = totalOfAverage / runCount;
                }

                float NewDeadline = Mathf.Clamp(Mathf.Ceil(__instance.profitQuota / quotaAverage), minimumDays, maximumDays);

                __instance.AddXDaysToDeadline(NewDeadline);

                DynamicDeadlineModPlugin.Instance.mls.LogInfo($"This person is the host, changing deadline. DailyValue registered as {dynamicDifficulty}, new average is {quotaAverage}, and host is currently on their {runCount} run!");
                DynamicDeadlineModPlugin.Instance.mls.LogInfo($"The new deadline is {NewDeadline} days.");
                totalOfAverage += dynamicDifficulty;
                previousDeadline = NewDeadline;
                DynamicDeadlineModPlugin.Instance.mls.LogInfo($"Did the value get assigned properly? Previous deadline is {previousDeadline}");
                ES3.Save("previousDeadline", previousDeadline, currentSaveFile);
                ES3.Save("totalOfAverage", totalOfAverage, currentSaveFile);
            }
            else if (isHost && LethalConfigHelper.legacyCal.Value == true)
            {
                float NewDeadline = Mathf.Clamp(Mathf.Ceil(__instance.profitQuota / LethalConfigHelper.legacyDailyValue.Value), minimumDays, maximumDays);
                __instance.AddXDaysToDeadline(NewDeadline);
                DynamicDeadlineModPlugin.Instance.mls.LogInfo("This person is the host and using the legacy difficulty calculations. Changing deadline.");
            }
            else
            {
                DynamicDeadlineModPlugin.Instance.mls.LogInfo("This person is not the host. Will not change deadline or send rpc.");
                return;
            }
        }
    }
}
