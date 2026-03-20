using HarmonyLib;
using System;
using UnityEngine;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(ItemPlaceZone))] // ref https://www.youtube.com/watch?v=ETMomo8rhCM
    public class PedestalFix
    {
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
                comp.GetComponent<Rigidbody>().isKinematic = false;
                force.enabled = true;
                comp.GetComponent<Rigidbody>().angularVelocity = force.force;
                UnityEngine.Object.Instantiate(MonoSingleton<HookArm>.Instance.errorSound);
                MonoSingleton<CameraController>.Instance.CameraShake(0.5f);
                int rand = new System.Random().Next(1, 6);
                switch (rand)
                    {
                    case 1:
                        MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("<color=red>That doesnt belong there!</color>", "", "", 0, silent: true);
                        break;
                    case 2:
                        MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("<color=red>Stop that!</color>", "", "", 0, silent: true);
                        break;
                    case 3:
                        MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("<color=red>This is for your safety!</color>", "", "", 0, silent: true);
                        break;
                    case 4:
                        MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("<color=red>You'll get softlocked!</color>", "", "", 0, silent: true);
                        break;
                    case 5:
                        MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("<color=red>Quit doing that!</color>", "", "", 0, silent: true);
                        break;
                    case 6:
                        MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("<color=red>That doesnt fit there!</color>", "", "", 0, silent: true);
                        break;
                }
            }
        }
    }
}
