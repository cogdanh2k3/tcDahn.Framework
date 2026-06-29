using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace tcDahn
{
    public class DDebug : MonoBehaviour
    {
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object message)
        {
            Debug.Log(message);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogError(object message)
        {
            Debug.LogError(message);
        }


    }
}
