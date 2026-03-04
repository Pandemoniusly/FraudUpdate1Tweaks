using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(Explosion))]
    public class ExplosionFix
    {
        public static int frame = 0; // per explosion, only accessable in this class
        [HarmonyPatch("CheckPortalOverlaps")]
        [HarmonyPrefix]
        private static bool PortalChecker(Explosion __instance)
        {
            frame = frame + 1;
            if (frame < 5)
            {
                frame = 0;
                return false;
            }
            else return true;
        }
    }
}
