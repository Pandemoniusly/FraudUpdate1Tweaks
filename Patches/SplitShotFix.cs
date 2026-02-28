using HarmonyLib;
using MonoMod.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography;
using ULTRAKILL.Enemy;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RemoteConfigSettingsHelper;
using static UnityEngine.SendMouseEvents;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(RevolverBeam))]
    public class Revolver
    {
        [HarmonyPatch("PiercingShotOrder")]
        [HarmonyPrefix]
        private static void RevolverPierce(RevolverBeam __instance,ref PhysicsCastResult rayHit, ref bool ___splitcoinable, ref LayerMask ___enemyLayerMask)
        {
            bool setSplit = false;
            if (!__instance.strongAlt)
            {
                ___splitcoinable = true;
                setSplit = true;
            }
            if (setSplit || ___splitcoinable)
            {

                Vector3 position = __instance.transform.transform.position;
                Vector3 forward = __instance.transform.transform.forward;
                float num2 = 1000f;
                PortalTraversalV2[] portalTraversals;
                Vector3 endPoint;
                PhysicsCastResult hitInfo;
                bool flag2 = PortalPhysicsV2.Raycast(position, forward, num2, ___enemyLayerMask, out hitInfo, out portalTraversals, out endPoint);
                if (flag2 & hitInfo.transform.gameObject.TryGetComponent<Coin>(out var coin))
                {
                    Vision coinVision = AccessTools.Field(typeof(Coin), "vision").GetValue(coin) as Vision;
                        VisionQuery enemyQuery = AccessTools.Field(typeof(Coin), "enemyQuery").GetValue(coin) as VisionQuery;
                        coinVision.UpdateSourcePos(coin.transform.position);
                        TargetDataRef Data;
                        bool CanSee = coinVision.TrySee(enemyQuery, out Data);
                        if (!CanSee || coinVision.visionIndex == -1)
                        {
                            ___splitcoinable = false;
                        }
                }
            }
        }
    }
}