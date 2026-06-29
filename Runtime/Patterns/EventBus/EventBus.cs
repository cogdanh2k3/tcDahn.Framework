using System;
using UnityEngine;

namespace tcDahn
{
    public static class EventBus<T> where T : IEvent
    {
        private static Action<T> _action = delegate { };

        public static void Subscribe(Action<T> listener)
        {
            _action += listener;
        }

        public static void Unsubscribe(Action<T> listener)
        {
            _action -= listener;
        }

        public static void Post(T @event)
        {
            _action.Invoke(@event);
        }
    }
}
