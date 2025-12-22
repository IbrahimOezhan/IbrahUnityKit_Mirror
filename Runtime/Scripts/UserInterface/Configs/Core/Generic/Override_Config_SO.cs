using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class Override_Config_SO<T> : Override_Config_SO_Base where T : ScriptableObject
    {
        [SerializeField] private bool overrideValue;

        [SerializeField, ShowIf(nameof(GetOverride))]
        private T value;

        protected virtual bool GetOverride()
        {
            return overrideValue;
        }

        public bool TryGet(out T value)
        {
            value = Get();
            return GetOverride();
        }

        public T Get()
        {
            return GetOverride() ? this.value : default(T);
        }
    }
}