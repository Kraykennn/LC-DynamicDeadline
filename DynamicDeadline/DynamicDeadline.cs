using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using DynamicDeadlineMod.Patches;
using BepInEx.Configuration;
using System.Runtime.CompilerServices;

namespace DynamicDeadlineMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class DynamicDeadlineMod : BaseUnityPlugin
    {
        private const string modGUID = "Haha.DynamicDeadline";
        private const string modName = "Dynamic Deadline";
        private const string modVersion = "1.2.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static DynamicDeadlineMod Instance;

        static internal ConfigEntry<float> MinScrapValuePerDay;

        static internal ConfigEntry<bool> incrementalCal;

        static internal ConfigEntry<float> incrementalDailyValue;

        static internal ConfigEntry<bool> useMinMax;

        static internal ConfigEntry<float> setMinimumDays;

        static internal ConfigEntry<float> setMaximumDays;

        internal ManualLogSource mls;

        internal void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modName);

            mls.LogInfo("No more short deadlines for excessive quotas.");

            MinScrapValuePerDay = Config.Bind("Customizable Values", "Minimum Daily ScrapValue", 200f, "Set this value to the minimum scrap value you should achieve per day. This will ignore the calculation for daily scrap if it's below this number.");

            useMinMax = Config.Bind("Customizable Values", "Use Custom Deadline Range", false, "Set to true if you want to use the custom minimum/maximum deadline range.");

            setMinimumDays = Config.Bind("Customizable Values", "Minimum Deadline", 3f, "If Use Custom Deadline Range is enabled, this is the minimum deadline you will have.");

            setMaximumDays = Config.Bind("Customizable Values", "Maximum Deadline", float.MaxValue, "If use Custom Deadline Range is enabled, this is the maximum deadline you will have.");

            incrementalCal = Config.Bind("Customizeable Values - Incremental", "Incremental Calculations", false, "Toggle this option to activate deadline-based calculation for incrementally raising the MinScrapValuePerDay each time you meet a quota, ensuring a slower increase in the amount of days. This will Disable the default bevahiour of increasing this based on average of your daily scrap.");

            incrementalDailyValue = Config.Bind("Customizable Values", "Incremental Daily Value", 30f, "If Use incremental minimum daily ScrapValue, this is the amount it will increase every time a quota is complete.");

            harmony.PatchAll(typeof(DynamicDeadlineMod));
            harmony.PatchAll(typeof(ProfitQuotaPatch));
        }

    }

}
