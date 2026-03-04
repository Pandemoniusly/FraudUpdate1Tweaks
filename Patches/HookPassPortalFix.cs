using HarmonyLib;
using ULTRAKILL.Portal;
using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;
using ULTRAKILL.Portal.Native;

namespace FraudTweaks.Patches
{
/*    [HarmonyPatch(typeof(Enemy))]
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
        public static int frame = 0;
        public static bool wasInPortal = false;
        [HarmonyPatch("FixedUpdate")]
        [HarmonyPostfix]
        private static void PortalPass(HookArm __instance,ref EnemyIdentifier ___caughtEid,ref HookPoint ___caughtHook, ref Collider ___caughtCollider, ref bool ___lightTarget,ref Vector3 ___hookPoint, ref CapsuleCollider ___playerCollider)
        {
            // failing tremendosly at this trying to fix it, not fully implemented, a is
            if (___caughtHook != null)
            {
                Vector3 vector5 = ___playerCollider.ClosestPoint(___hookPoint);
                Vector3 relative = ___hookPoint + (___caughtCollider.ClosestPoint(vector5));
                if (Vector3.Distance(vector5, relative) < 0.25f || (!___lightTarget && Vector3.Distance(vector5 + MonoSingleton<NewMovement>.Instance.rb.velocity * Time.fixedDeltaTime, relative) < 0.25f))
                {
                    FraudTweaks.CancelWhiplash = true;
                }
            }
            if (!___lightTarget)
            {
                FraudTweaks.CaughtIdentifier = ___caughtEid;
            }
            // a
            if (FraudTweaks.CancelWhiplash)
            {
                __instance.Cancel();
                FraudTweaks.CancelWhiplash = true;
            }
            // a
        }

        private static bool PrecisePortalCheck(PortalScene scene, PortalHandle portalHandle, Vector3 startPosition, Vector3 closestPoint)
        {
            Vector3 end = scene.GetTravelMatrix(portalHandle.Reverse()).MultiplyPoint3x4(closestPoint);
            if (scene.FindPortalBetween(startPosition, end, out var hitPortal, out var _, out var _))
            {
                return hitPortal == portalHandle;
            }
            return false;
        }
    }*/
}
