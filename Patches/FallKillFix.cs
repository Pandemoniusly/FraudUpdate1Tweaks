using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(MaliciousFace))] // ref https://www.youtube.com/watch?v=ETMomo8rhCM
    public class MaliciousFix
    {
        [HarmonyPatch("TriggerHit")]
        [HarmonyPrefix]
        private static bool FallCheck(MaliciousFace __instance, ref Collider other, ref bool ___spiderFalling)
        {
            EnemyIdentifier comp = other.GetComponent<EnemyIdentifier>();
            if (comp != null)
            {
                if (___spiderFalling & (comp.enemyType == EnemyType.Deathcatcher || comp.enemyType == EnemyType.Idol))
                {
                    Vector3 dir = other.ClosestPoint(__instance.transform.position);
                    RaycastHit[] cast = Physics.RaycastAll(__instance.transform.position, dir, Vector3.Distance(__instance.transform.position, dir), LayerMaskDefaults.Get(LMD.Environment), QueryTriggerInteraction.Ignore);
                    if (cast.Length > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    /*[HarmonyPatch(typeof(DeathZone))]
    public class GutterFix
    {
        [HarmonyPatch("GotHit")]
        [HarmonyPrefix]
        private static bool GutterFallCheck(DeathZone __instance, ref Collider other, ref bool ___enemyAffected)
        {
            if (__instance.deathType == "PANCAKED")
            {
                IgnoreDeathZones component2;
                if ((other.gameObject.CompareTag("Enemy") || other.gameObject.layer == 10 || other.gameObject.layer == 11 || other.gameObject.layer == 12) && ___enemyAffected && !other.TryGetComponent<IgnoreDeathZones>(out component2))
                {
                    EnemyIdentifier enemyIdentifier = other.gameObject.GetComponentInParent<EnemyIdentifier>();
                    if (enemyIdentifier == null && other.gameObject.TryGetComponent<IdolMauricer>(out var _))
                    {
                        enemyIdentifier = other.gameObject.GetComponentInParent<EnemyIdentifier>();
                    }
                    if (enemyIdentifier.enemyType == EnemyType.Idol || enemyIdentifier.enemyType == EnemyType.Deathcatcher)
                    {
                        enemyIdentifier.InstaKill();
                        return true;
                    }
                }
            }
            return false;
        }
    }*/
}
