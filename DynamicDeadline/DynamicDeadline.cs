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

        static internal ConfigEntry<bool> legacyCal;

        static internal ConfigEntry<float> legacyDailyValue;

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

            legacyCal = Config.Bind("Customizeable Values - Legacy", "Legacy Calculations", false, "Set to true if you want to use the deadline calculation from 1.1.0 prior.");

            legacyDailyValue = Config.Bind("Customizeable Values - Legacy", "Daily Scrap Value", 200f, "Set this number to the value of scrap you can reasonably achieve in a single day.");

            harmony.PatchAll(typeof(DynamicDeadlineMod));
            harmony.PatchAll(typeof(ProfitQuotaPatch));
        }

    }

}
