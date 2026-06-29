using System;
using System.Collections.Generic;
using UnityEngine;

namespace tcDahn
{
    internal class DataAutoSave : MonoBehaviour
    {
        private static DataAutoSave _instance;
        private readonly List<Action> _callbacks = new();

        internal static void Register(Action saveCallback)
        {
            if (_instance == null)
            {
                var go = new GameObject("[DataAutoSave]");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<DataAutoSave>();
            }
            _instance._callbacks.Add(saveCallback);
        }

        private void SaveAll()
        {
            foreach (var cb in _callbacks)
            {
                cb();
            }
        }

        private void OnApplicationPause(bool paused)
        {
            if (paused)
                SaveAll();
        }

        private void OnApplicationQuit()
        {
            SaveAll();
        }
    }
}
