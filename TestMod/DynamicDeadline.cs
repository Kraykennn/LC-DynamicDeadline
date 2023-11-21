using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using DynamicDeadlineMod.Patches;

namespace DynamicDeadlineMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class DynamicDeadline : BaseUnityPlugin
    {
        private const string modGUID = "Haha.DynamicDeadline";
        private const string modName = "Test Mod";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static DynamicDeadline Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("The mod has awoken.");

            harmony.PatchAll(typeof(DynamicDeadline));
            harmony.PatchAll(typeof(ProfitQuotaPatch));
        }


    }
}
