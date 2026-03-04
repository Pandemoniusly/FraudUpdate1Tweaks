using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(ItemPlaceZone))] // ref https://www.youtube.com/watch?v=ETMomo8rhCM
    public class PedestalFix
    {
        public static ItemIdentifier Item = null;
        [HarmonyPatch("CheckItem")]
        [HarmonyPrefix]
        private static void RemoveItem(ItemPlaceZone __instance, ref AudioSource ___soundOnDeactivated)
        {
            ItemIdentifier comp = __instance.GetComponentInChildren<ItemIdentifier>();
            if (comp == null) return;
            if (comp.transform.name.Contains("VendingMachine"))
            {
                AddForce force = comp.GetComponent<AddForce>();
                force.force = (__instance.transform.up * 2) + new Vector3(UnityEngine.Random.Range(-3, 3), 0, UnityEngine.Random.Range(-3, 3)).normalized * 1;
                force.oneTime = false;
                force.relative = false;
                force.onEnable = true;
                force.enabled = false;
                comp.transform.SetParent(null, true);
                Item = comp;
            }
        }

        [HarmonyPatch("CheckItem")]
        [HarmonyPostfix]
        private static void RemoveItem2(ItemPlaceZone __instance)
        {
            FraudTweaks.Logger.LogInfo(Item.name);
            Item.GetComponent<Rigidbody>().isKinematic = false;
            AddForce force = Item.GetComponent<AddForce>();
                force.enabled = true;
            Item.GetComponent<Rigidbody>().angularVelocity = force.force;
        }
    }
}
