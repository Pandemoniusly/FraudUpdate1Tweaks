using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FraudTweaks.Patches
{
    [HarmonyPatch(typeof(SpawnMenu))]
    public class CubeMaker
    {

        [HarmonyPatch("Awake")]
        [HarmonyPrefix]
        public static void CubePrefix(SpawnMenu __instance, ref SpawnableObjectsDatabase ___objects)
        {
            SpawnableObject[] list = ___objects.sandboxTools;
            for (int i = 0; i < list.Length; i++)
            {
                FraudTweaks.Logger.LogInfo(list[i].objectName);
                if (list[i].objectName == "Block Creator")
                {
                    list[i].sandboxOnly = false;
                }
            }
        }
    }
    [HarmonyPatch(typeof(SandboxUtils))]
    public class CubeInfo
    {
        [HarmonyPatch("CreateFinalBlock")]
        [HarmonyPostfix]
        public static void LogCube(SpawnMenu __instance, ref GameObject __result)
        {
            BoxCollider boxCollider = __result.GetComponent<BoxCollider>();
            FraudTweaks.Logger.LogWarning("Scale = " + boxCollider.size + System.Environment.NewLine +
                "Center = " + (boxCollider.center + __result.transform.position));
        }
    }
}
