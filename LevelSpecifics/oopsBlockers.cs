using FraudTweaks.Patches;
using GameConsole;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FraudTweaks.LevelSpecifics
{
    [HarmonyPatch(typeof(StatsManager))]
    public class LevelFinder
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void Started(StatsManager __instance)
        {
            GameObject[] All = Resources.FindObjectsOfTypeAll<GameObject>();
            List<GameObject> oobs = All.Where(obj => obj.name.Contains("OopsBlocker")).ToList();
            foreach (GameObject obj in oobs)
            {
                FraudTweaks.OutofboundsActive.Add(obj,obj.activeSelf);
                ActiveCheck active = obj.AddComponent<ActiveCheck>();
                active.instance = active;
                Bounds bounds = new Bounds();
                foreach (BoxCollider obj2 in obj.GetComponentsInChildren<BoxCollider>())
                {
                    bounds.Encapsulate(obj2.bounds);
                }
                bounds.Expand(-0.05f);
                FraudTweaks.OutofboundsList.Add(bounds);
                FraudTweaks.OutofboundsParents.Add(obj.transform);
            }
            
        }
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static void BoundsRunner(StatsManager __instance)
        {
            for (int i = 0; i < FraudTweaks.OutofboundsList.Count; i++)
            {
                Bounds playerBounds = MonoSingleton<NewMovement>.Instance.playerCollider.bounds;
                bool flag = false;
                foreach (BoxCollider obj2 in FraudTweaks.OutofboundsParents[i].GetComponentsInChildren<BoxCollider>())
                {
                    if (obj2.bounds.Contains(playerBounds.min) && obj2.bounds.Contains(playerBounds.max))
                    {
                        flag = true;
                        break;
                    }
                }
                KeyValuePair<GameObject, bool> active = FraudTweaks.OutofboundsActive.ElementAtOrDefault(i);
                if (active.Key != null)
                {
                    if (!flag & (FraudTweaks.OutofboundsList[i].Contains(playerBounds.min) && FraudTweaks.OutofboundsList[i].Contains(playerBounds.max)))
                    {
                        if (active.Value)
                        {
                            bool last = FraudTweaks.OutofboundsActive.ElementAt(i).Value;
                            FraudTweaks.OutofboundsParents[i].gameObject.SetActive(true);
                            FraudTweaks.OutofboundsActive[FraudTweaks.OutofboundsActive.ElementAt(i).Key] = last;
                        }
                    }
                    else
                    {
                        bool last = FraudTweaks.OutofboundsActive.ElementAt(i).Value;
                        FraudTweaks.OutofboundsParents[i].gameObject.SetActive(false);
                        FraudTweaks.OutofboundsActive[FraudTweaks.OutofboundsActive.ElementAt(i).Key] = last;
                    }
                }
            }
        }
    }
    public class ActiveCheck : MonoBehaviour
    {
        public ActiveCheck instance;
        public void OnEnable()
        {
            if (!FraudTweaks.OutofboundsActive.ContainsKey(instance.gameObject)) return;
            FraudTweaks.OutofboundsActive[instance.gameObject] = true;
        }
        public void OnDisable()
        {
            if (!FraudTweaks.OutofboundsActive.ContainsKey(instance.gameObject)) return;
            FraudTweaks.OutofboundsActive[instance.gameObject] = false;
        }
    }
}
