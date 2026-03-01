using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(Grenade))]
    public class Rocket
    {
        [HarmonyPatch("PlayerRideStart")]
        [HarmonyPostfix]
        private static void RideOn(Grenade __instance, ref GameObject ___interruptSphere)
        {
            if (___interruptSphere != null)
            {
                ___interruptSphere.SetActive(true);
            }
        }
    }

    [HarmonyPatch(typeof(ShotgunHammer))]
    public class JackhammerRocket
    {
        [HarmonyPatch("ImpactRoutine")]
        [HarmonyPrefix]
        private static void RocketPuncher(ShotgunHammer __instance)
        {
            Vector3 position = MonoSingleton<CameraController>.Instance.GetDefaultPos();
            Collider[] cols = Physics.OverlapSphere(position, 0.01f);
            if (cols.Length != 0)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    Transform transform = cols[i].transform;
                    if (transform.TryGetComponent<ParryHelper>(out var component))
                    {
                        transform = component.target;
                    }
                    if (MonoSingleton<ObjectTracker>.Instance.grenadeList.Count > 0 && transform.gameObject.layer == 10)
                    {
                        Grenade componentInParent = transform.GetComponentInParent<Grenade>();
                        if ((bool)componentInParent & componentInParent.rocket)
                        {
                            if (FraudTweaks.RocketDelay > 4f) // uses a static field in FraudTweaks.cs because a static field in the patched class wouldnt update and save otherwise
                            {                                 // 
                                GameObject interrupt = AccessTools.Field(typeof(Grenade), "interruptSphere").GetValue(componentInParent) as GameObject;
                                interrupt.SetActive(false);
                            }
                            else
                            {
                                GameObject interrupt = AccessTools.Field(typeof(Grenade), "interruptSphere").GetValue(componentInParent) as GameObject;
                                interrupt.SetActive(true);
                                FraudTweaks.RocketDelay = FraudTweaks.RocketDelay + 1;
                                if (componentInParent.levelledUp) FraudTweaks.RocketDelay = FraudTweaks.RocketDelay + 0.5f;
                            }
                        }
                    }
                }
            }
        }
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        private static void DelayFallOff(ShotgunHammer __instance)
        {
            FraudTweaks.RocketDelay = Mathf.Clamp(FraudTweaks.RocketDelay - Time.deltaTime, 0, 10);
        }
    }
}
