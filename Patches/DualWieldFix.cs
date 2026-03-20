using HarmonyLib;
using System;
using UnityEngine;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(GunControl))]
    public class YesWeaponFix
    {
        [HarmonyPatch("YesWeapon")]
        [HarmonyPostfix]
        private static void YesWeaponUpdate(GunControl __instance)
        {
            if (__instance.currentWeapon == null) return;
            __instance.ForceWeapon(__instance.currentWeapon, true);
        }
        [HarmonyPatch("NoWeapon")]
        [HarmonyPostfix]
        private static void NoWeaponUpdate(GunControl __instance, ref Action<GameObject> ___OnWeaponChange)
        {
            ___OnWeaponChange.Invoke(__instance.currentWeapon); // pretty much just invokes null since the weapon aint active
        }
    }
}
