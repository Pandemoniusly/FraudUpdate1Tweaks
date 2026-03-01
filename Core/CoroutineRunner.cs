using UnityEngine;
using System.Collections;
namespace FraudTweaks.Patches
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;
        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject();
                    _instance = gameObject.AddComponent<CoroutineRunner>();;
                    Object.DontDestroyOnLoad(gameObject);
                }
                return _instance;
            }
        }
        public Coroutine RunCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }
    }
}
