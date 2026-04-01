using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FraudTweaks.LevelSpecifics
{
    /*[HarmonyPatch(typeof(MaliciousFace))] // ref https://www.youtube.com/watch?v=ETMomo8rhCM
    public class PedestalFix
    {
        [HarmonyPatch("TriggerHit")]
        [HarmonyPrefix]
        private static bool FallCheck(MaliciousFace __instance, ref Collider other, ref bool ___falling)
        {
            if (___falling)
            {
                Vector3 dir = other.ClosestPoint(__instance.transform.position);
                RaycastHit[] cast = Physics.RaycastAll(__instance.transform.position, dir, Vector3.Distance(__instance.transform.position, dir), LayerMaskDefaults.Get(LMD.Environment), QueryTriggerInteraction.Ignore);
                if (cast.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }*/
}
