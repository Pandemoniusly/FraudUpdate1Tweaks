using BepInEx;
using BepInEx.Logging;
using FraudTweaks.Patches;
using HarmonyLib;
using System.Collections.Generic;
using ULTRAKILL.Enemy;
using UnityEngine;

namespace FraudTweaks
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class FraudTweaks : BaseUnityPlugin
    {
        public static FraudTweaks Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }
        public static float RocketDelay = 0f;
        public static bool CancelWhiplash = false;
        public static EnemyIdentifier CaughtIdentifier;
        public static HookPoint SavedHook;
        public static Dictionary<Shotgun,float> sawTimer = new Dictionary<Shotgun,float>();
        public static Dictionary<ItemIdentifier,Transform> LastParent = new Dictionary<ItemIdentifier,Transform>();
        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
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
