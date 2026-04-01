using HarmonyLib;
using UnityEngine;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(StatsManager))]
    public class LevelFinder
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void Started(StatsManager __instance)
        {
            if (__instance.levelNumber == 30)
            {
                /*object bound = BoundsMaker.MakeBounds(new Vector3(6.95f, 39.97f, 58.93f), new Vector3(194.41f, 130.2f, 456.53f));
                FraudTweaks.BoundsList.Add(bound);*/
                Transform trigger = GameObject.Find("Inside").transform.Find("9 - Archangel Hall").transform.Find("9 Nonstuff").transform.Find("TrapRoomTrigger");
                trigger.localScale += Vector3.right * 15;
                ObjectActivator[] activators = trigger.GetComponents<ObjectActivator>();
                activators[0].delay = 0.55f;
                activators[1].delay = 0.55f;
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.localScale = Vector3.one * 3;
                sphere.name = "Sphere";
                UnityEngine.Object.Instantiate(sphere);
            }
        }
    }
    /*public class BoundsMaker
    {
        public static Bounds MakeBounds(Vector3 scale, Vector3 center) // 
        {
            var bounds = new Bounds();
            bounds.size = scale;
            bounds.center = center;
            return bounds;
        }
        public static BoxCollider MakeBoxCollider(Vector3 scale, Vector3 center) // 
        {
            var obj = Object.Instantiate(new GameObject());
            var bounds = obj.AddComponent<BoxCollider>();
            bounds.size = scale;
            bounds.center = center;
            return bounds;
        }
    }
    [HarmonyPatch(typeof(HookArm))]
    public class AntiHookBounds
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static void InBounding(HookArm __instance)
        {
            if (__instance.state != HookState.Throwing) return;
            if (MonoSingleton<FistControl>.Instance.heldObject == null) return;
            GameObject active = GameObject.Find("Inside");
            active = active.transform.Find("SwapToRed").gameObject.activeSelf ? active.transform.Find("SwapToRed").gameObject : active.transform.Find("SwapToBlue").gameObject;
            if (active.GetComponent<ObjectActivator>().enabled == false) return;
            for (int i = 0; i < FraudTweaks.BoundsList.Count; i++)
            {
                if (FraudTweaks.BoundsList[i].GetType() != typeof(Bounds)) return;
                Bounds bound = (Bounds)FraudTweaks.BoundsList[i];
                if (bound.IntersectRay(new Ray(__instance.transform.position, __instance.hook.position - __instance.transform.position), out var distance) && distance < Vector3.Distance(__instance.transform.position, __instance.hook.position) + 1f)
                {
                     Object.Instantiate(__instance.errorSound);
                     MonoSingleton<CameraController>.Instance.CameraShake(0.5f);
                     MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("<color=red>ERROR: GET CLOSER</color>", "", "", 0, silent: true);
                     __instance.StopThrow();
                }
            }
        }
    }*/
}
