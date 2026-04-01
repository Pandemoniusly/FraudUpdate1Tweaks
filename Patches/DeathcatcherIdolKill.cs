using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(EnemyIdentifier))]
    public class DeathcatcherKillFix
    {
        [HarmonyPatch("ProcessDeath")]
        [HarmonyPostfix]
        public static void FixDeath(EnemyIdentifier __instance)
        {
            if (__instance.enemyType == EnemyType.Deathcatcher || __instance.enemyType == EnemyType.Idol)
            {
                if (!__instance.TryGetComponent<Enemy>(out var component))
                {
                    AccessTools.Method(typeof(EnemyIdentifier), "GetGoreZone").Invoke(__instance, null);
                    if ((bool)__instance.gz.checkpoint)
                    {
                        __instance.gz.checkpoint.restartKills = __instance.gz.checkpoint.restartKills-1;
                    }
                }
            }
        }
    }
}
