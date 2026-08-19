using UnityEngine;

namespace tcDahn
{
    public class MonoBase : MonoBehaviour
    {
        private GameObject _gameObject;
        private Transform _transform;
        private bool _isStarted = false;

        public Transform TransformCached => _transform ??= transform;
        public GameObject GameObjectCached => _gameObject ??= gameObject;

        protected virtual void Start()
        {
            _isStarted = true;
            RegisterTick();
        }

        private void OnEnable()
        {
            if (_isStarted) RegisterTick();
        }

        private void OnDisable()
        {
            if (_isStarted) UnregisterTick();
        }


        private void RegisterTick()
        {
            MonoCallback.SafeInstance.EventUpdate += Tick;
            MonoCallback.SafeInstance.EventLateUpdate += LateTick;
            MonoCallback.SafeInstance.EventFixedUpdate += FixedTick;
        }

        private void UnregisterTick()
        {
            if (MonoCallback.IsDestroyed) return;
            MonoCallback.Instance.EventUpdate -= Tick;
            MonoCallback.Instance.EventLateUpdate -= LateTick;
            MonoCallback.Instance.EventFixedUpdate -= FixedTick;
        }

        protected virtual void Tick() { }
        protected virtual void LateTick() { }
        protected virtual void FixedTick() { }

    }
}
