using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using DynamicDeadlineMod.Patches;
using DynamicDeadlineMod.Helpers;

namespace DynamicDeadlineMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("ainavt.lc.lethalconfig")]
    public class DynamicDeadlineModPlugin : BaseUnityPlugin
    {
        private const string modGUID = "Haha.DynamicDeadline";
        private const string modName = "Dynamic Deadline";
        private const string modVersion = "1.2.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static DynamicDeadlineModPlugin Instance;

        public ManualLogSource mls;

        internal void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modName);

            mls.LogInfo("No more short deadlines for excessive quotas.");

            LethalConfigHelper.SetLehalConfig(Config);

            harmony.PatchAll(typeof(DynamicDeadlineModPlugin));
            harmony.PatchAll(typeof(ProfitQuotaPatch));
        }

    }

}
