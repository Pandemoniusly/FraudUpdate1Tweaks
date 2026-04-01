using BepInEx;
using BepInEx.Logging;
using FraudTweaks.Patches;
using HarmonyLib;
using System.Collections.Generic;
using ULTRAKILL.Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FraudTweaks
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class FraudTweaks : BaseUnityPlugin
    {
        public static FraudTweaks Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }
        public static Dictionary<ItemPlaceZone,ItemIdentifier> LastItem = new Dictionary<ItemPlaceZone,ItemIdentifier>();
        public static int SavedLevel = 0;
        public static List<Bounds> OutofboundsList = new List<Bounds>();
        public static List<Transform> OutofboundsParents = new List<Transform>();
        public static Dictionary<GameObject,bool> OutofboundsActive = new Dictionary<GameObject, bool>();
        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
            SceneManager.sceneLoaded += (Scene scene, LoadSceneMode lsm) =>
            {
                OutofboundsList.Clear();
                OutofboundsParents.Clear();
                OutofboundsActive.Clear();
                OutofboundsList.Capacity = 0;
                OutofboundsParents.Capacity = 0;
            };
            }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }
    }
}
