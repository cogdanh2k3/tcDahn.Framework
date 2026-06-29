using UnityEngine;

namespace tcDahn
{
    [DefaultExecutionOrder(-50)]
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {

        [SerializeField] private bool _persistAcrossScenes = true;
        protected virtual bool PersistAcrossScenes => _persistAcrossScenes;

        private static T _instance;
        private static readonly object _lock = new object();

        private static bool _applicationIsQuitting = false;

        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                    return null;

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindFirstObjectByType<T>();
                        if (_instance == null)
                        {
                            Instantiate();
                        }
                    }
                }

                
                return _instance;
            }
        }

        public static bool HasInstance => _instance != null && !_applicationIsQuitting;

        private static void Instantiate()
        {
            if (HasInstance)
            {
                return;
            }
            var name = typeof(T).FullName;
            var go = new GameObject(typeof(T).Name);
            _instance = go.AddComponent<T>();
        }

        #region MonoBehavior
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
                if (PersistAcrossScenes)
                {
                    transform.SetParent(null);
                    DontDestroyOnLoad(gameObject);
                }
            } 
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
                _applicationIsQuitting = true;
            }
        }
        #endregion
    }
}
