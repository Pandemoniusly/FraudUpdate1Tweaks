using HarmonyLib;
using UnityEngine;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(Drone))] // funny as hell this happens
    public class ProvidenceFix
    {
        [HarmonyPatch("Explode")]
        [HarmonyPrefix]
        public static void GhostCheck(Drone __instance, ref EnemyIdentifier ___eid, ref GameObject ___ghost)
        {
            if (___eid.enemyType == EnemyType.Providence)
            {
                ___ghost = null;
            }
        }
    }
}
