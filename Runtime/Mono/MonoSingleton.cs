using System.Diagnostics;
using System.Threading;
using UnityEngine;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;

namespace tcDahn
{
    [DefaultExecutionOrder(-50)]
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected abstract bool PersistAcrossScenes { get; }

        private static T _instance;

        public static bool IsDestroyed { get; private set; }
        public static bool HasInstance => _instance != null && !IsDestroyed;

        public static T Instance
        {
            get
            {
                if (IsDestroyed)
                    return null;

                if (_instance == null)
                    _instance = FindFirstObjectByType<T>();
                return _instance;
            }
        }


        #region MonoBehavior
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                IsDestroyed = false;
                _instance = this as T;

                if (PersistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            } 
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance != this)
                return;

            _instance = null;
            IsDestroyed = true;
        }
        #endregion
    }
}
