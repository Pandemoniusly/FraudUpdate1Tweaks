using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using ULTRAKILL.Portal;
using ULTRAKILL.Portal.Geometry;
using Unity.Collections;
using UnityEngine;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(Explosion))]
    public class ExplosionFix
    {
        /*public static int frame = 0; // per explosion, only accessable in this class
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
        }*/
        private static readonly LayerMask mask = 1 << LayerMask.NameToLayer("Portal");
        private static Collider[] LastBuffer = new Collider[32];
        [HarmonyPatch("CheckPortalOverlaps")]
        [HarmonyPrefix]
        private static bool PortalChecker(Explosion __instance, ref float radius,ref PortalScene scene, ref Collider[] ___PortalOverlapBuffer)
        {
            UnityEngine.Vector3 position = __instance.transform.position;
            Collider[] colliders = Physics.OverlapSphere(position, radius, mask);      
            if (colliders.Any() & ___PortalOverlapBuffer != LastBuffer)                
            {                                                                          
                LastBuffer = ___PortalOverlapBuffer;                                   
                foreach (Collider col in colliders)                                    
                {                                                                      
                    PortalHandle handle = col.GetComponent<PortalIdentifier>().Handle; 
                    PortalTransform portalTransform = scene.GetPortalObject(handle).GetTransform(handle.side.Reverse());
                    if (!portalTransform.IsPointInFront(position) || UnityEngine.Vector3.Dot(portalTransform.back, position - portalTransform.center) >= radius)
                    {
                        continue;
                    }
                    PortalHandleSequence travelHandles = new PortalHandleSequence(handle);
                    UnityEngine.Vector3 vector = scene.GetTravelMatrix(in travelHandles).MultiplyPoint3x4(position);
                    int num = Physics.OverlapSphereNonAlloc(vector, radius, ___PortalOverlapBuffer, LayerMaskDefaults.Get(LMD.EnemiesAndPlayer));
                    for (int i = 0; i < num; i++)
                    {
                        Collider collider = ___PortalOverlapBuffer[i];
                        UnityEngine.Vector3 closestPoint = collider.ClosestPoint(vector);
                        var check = AccessTools.Method(typeof(Explosion), "PrecisePortalCheck");
                        if ((bool)check.Invoke(__instance,new object[] { scene, handle, position, closestPoint }))
                        {
                            var collide = AccessTools.Method(typeof(Explosion), "Collide");
                            collide.Invoke(__instance,new object[] { collider, vector });
                        }
                    }
                }
            }
            return true;
	    }
    }
}
