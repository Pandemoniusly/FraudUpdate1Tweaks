using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ULTRAKILL.Portal;
using ULTRAKILL.Portal.Geometry;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.StylePropertyAnimationSystem;

namespace FraudTweaks.Patches // not implemented, see comment below
{
    [HarmonyPatch(typeof(Enemy))]
    public class EnemyPassPortal
    {
        [HarmonyPatch("OnTravel")]
        [HarmonyPostfix]
        private static void PortalPassEnemy(Enemy __instance, PortalTravelDetails details)
        {
            if (FraudTweaks.CaughtIdentifier == __instance)
            {
                if (!details.isIntersectTraversal)
                {
                    FraudTweaks.CancelWhiplash = true;
                }
            }
        }
    }
    [HarmonyPatch(typeof(HookArm))]
    public class Hookee
    {
        [HarmonyPatch("FixedUpdate")]
        [HarmonyPostfix]
        private static void PortalPass(HookArm __instance,ref EnemyIdentifier ___caughtEid, ref Vector3 ___hookPoint,ref Transform ___caughtTransform)
        {
            // if i had a Physics.OverlapSphere for portals that also told me if it is intersecting the portal this would be alot easier for hookpoints
            // i dont think im competent enough for it yet
                FraudTweaks.CaughtIdentifier = ___caughtEid;
            if (FraudTweaks.CancelWhiplash)
            {
                __instance.Cancel();
                FraudTweaks.CancelWhiplash = true;
            }
        }
    }
}
