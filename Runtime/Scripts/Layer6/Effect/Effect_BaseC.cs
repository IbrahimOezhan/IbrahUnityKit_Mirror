#region

using System;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace IbrahKit.Effects
{
    [Serializable]
    public abstract class Effect_BaseC
    {
        [SerializeField]
        protected string key;

        private UnityEvent OnAddEvent = new();
        private UnityEvent OnRemoveEvent = new();

        public string GetKey()
        {
            return key;
        }

        public virtual void OnAdd()
        {
            OnAddEvent.Invoke();
        }

        public virtual void OnRemove()
        {
            OnRemoveEvent.Invoke();
        }

        public abstract void Run();

        public abstract int GetOrder();

        public abstract string GetEffectDescription();

        public abstract int CompareTo(Effect_BaseC comparer);

        public (UnityEvent, UnityEvent) GetEvents()
        {
            if (OnAddEvent == null || OnRemoveEvent == null)
            {
                Debug.LogWarning("Unity events are null");
            }

            return (OnAddEvent != null ? OnAddEvent : new(), OnRemoveEvent != null ? OnRemoveEvent : new());
        }
    }
}