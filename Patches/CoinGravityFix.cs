using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(Coin))]
    public class CoinFix
    {
        static IEnumerator FixedUpdateWaiter(Coin __instance) // fix for inconsistent bounces,
        {
            yield return new WaitForFixedUpdate();
            Rigidbody rigid = __instance.GetComponent<Rigidbody>();
            CustomGravity grav = __instance.GetComponent<CustomGravity>();
            if (rigid != null)
            {
                if (grav != null)
                {
                    rigid.isKinematic = false;
                    rigid.useGravity = false;
                    grav.useGravity = true;
                    rigid.velocity = Vector3.zero;
                    rigid.AddForce(Vector3.zero, ForceMode.VelocityChange);
                    rigid.AddForce(-grav.gravity.normalized * 25f, ForceMode.VelocityChange);
                }
            }
        }
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void BounceFix(Coin __instance) // bouncing on it silly style
        {
            Rigidbody rigid = __instance.GetComponent<Rigidbody>();
            if (rigid & rigid.useGravity & __instance.name.Contains("NewCoin+"))
            {
                CoroutineRunner.Instance.RunCoroutine(FixedUpdateWaiter(__instance));
            }
        }
    }
}
