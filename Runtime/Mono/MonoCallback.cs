using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace tcDahn
{
    [DefaultExecutionOrder(-100)]
    public class MonoCallback : MonoSingleton<MonoCallback>
    {
        protected override bool PersistAcrossScenes => true;

        public event Action EventUpdate;
        public event Action EventLateUpdate;
        public event Action EventFixedUpdate;
        public event Action<bool> EventApplicationPause;
        public event Action<bool> EventApplicationFocus;
        public event Action EventApplicationQuit;
        public event Action<Scene, Scene> EventActiveSceneChanged;

        public static MonoCallback SafeInstance
        {
            get
            {
                if (!HasInstance)
                {
                    var go = new GameObject("[MonoCallback]");
                    return go.AddComponent<MonoCallback>();
                }
                return Instance;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            SceneManager.activeSceneChanged += SceneManager_ActiveSceneChanged;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            SceneManager.activeSceneChanged -= SceneManager_ActiveSceneChanged;
        }

        private void Update()
        {
            EventUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            EventLateUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            EventFixedUpdate?.Invoke();
        }
        private void OnApplicationPause(bool pauseStatus)
        {
            EventApplicationPause?.Invoke(pauseStatus);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            EventApplicationFocus?.Invoke(hasFocus);
        }

        private void OnApplicationQuit()
        {
            EventApplicationQuit?.Invoke();
        }

        private void SceneManager_ActiveSceneChanged(Scene scenePrevious, Scene sceneCurrent)
        {
            EventActiveSceneChanged?.Invoke(scenePrevious, sceneCurrent);
        }
    }
}
