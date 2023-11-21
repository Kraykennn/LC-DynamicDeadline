using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace DynamicDeadlineMod.Patches
{
    [HarmonyPatch(typeof(TimeOfDay))]
    public class ProfitQuotaPatch
    {
        [HarmonyPatch(nameof(TimeOfDay.SetNewProfitQuota))]
        [HarmonyPostfix]
        static void DynamicDeadline(ref float timeUntilDeadline, ref float profitQuota)
        {
            timeUntilDeadline = Mathf.Clamp(Mathf.Ceil(profitQuota / 200), 4f, 10f);
        }



    }
}
