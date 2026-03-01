using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ULTRAKILL.Enemy;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(Coin))]
    public class CoinFix // not implemented yet into mod, sends punched coins twice as high, unsure what to do about that
    {
        private static void Bounce(Coin __instance) // bouncing on it silly style
        {
            Rigidbody rigid = __instance.GetComponent<Rigidbody>();
            CustomGravity grav = __instance.GetComponent<CustomGravity>();
            if (rigid != null & grav != null)
            {
                grav.useGravity = true;
                grav.gravity = grav.gravity.normalized * 49;
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.zero, ForceMode.VelocityChange);
                rigid.AddForce(-grav.gravity.normalized * 25f, ForceMode.VelocityChange);
            }
        }
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void BounceFix(Coin __instance)
        {
            Rigidbody rigid = __instance.GetComponent<Rigidbody>();
            if (rigid & rigid.useGravity & __instance.name.Contains("NewCoin+"))
            {
                rigid.isKinematic = false;
                rigid.useGravity = false;
                Bounce(__instance);
            }
        }
    }
}
