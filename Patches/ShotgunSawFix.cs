using HarmonyLib;
using System.Collections;
using UnityEngine;
using System;
namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(Shotgun))]
    public class SawFix
    {
        public static IEnumerator ShotgunRunner(object __instance, float delay)
        {
            yield return new WaitForSeconds(delay);
            Type shot = __instance.GetType();
            AccessTools.Method(shot, "ShootSaw").Invoke(__instance, new object[] { false }); // ok so, pretty sure the problem is cause its trying to run a method with paramaters in invoke and it gets all mad and doesnt fire cause of it, so we invoke with paramaters
        }

        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        private static void SawCheckNormal(Shotgun __instance, ref bool ___gunReady, ref GunControl ___gc, ref bool ___charging,ref WeaponIdentifier ___wid)
        {
            if ((MonoSingleton<InputManager>.Instance.InputSource.Fire2.WasCanceledThisFrame || (!MonoSingleton<InputManager>.Instance.PerformingCheatMenuCombo() && !GameStateManager.Instance.PlayerInputLocked && MonoSingleton<InputManager>.Instance.InputSource.Fire1.WasPerformedThisFrame)) && __instance.variation == 2 && ___gunReady && ___gc.activated && ___charging)
            {
                if (___wid.delay != 0)
                {
                    CoroutineRunner.Instance.RunCoroutine(ShotgunRunner(__instance,___wid.delay));
                }
            }
        }
    }
    [HarmonyPatch(typeof(ShotgunHammer))]
    public class SawFixHammer
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        private static void SawCheckHammer(ShotgunHammer __instance, ref bool ___gunReady, ref GunControl ___gc, ref bool ___charging, ref WeaponIdentifier ___wid)
        {
            if ((MonoSingleton<InputManager>.Instance.InputSource.Fire2.WasCanceledThisFrame || (!MonoSingleton<InputManager>.Instance.PerformingCheatMenuCombo() && !GameStateManager.Instance.PlayerInputLocked && MonoSingleton<InputManager>.Instance.InputSource.Fire1.WasPerformedThisFrame)) && __instance.variation == 2 && ___gunReady && ___gc.activated && ___charging)
            {
                if (___wid.delay != 0)
                {
                    CoroutineRunner.Instance.RunCoroutine(SawFix.ShotgunRunner(__instance, ___wid.delay));
                }
            }
        }
    }
}
