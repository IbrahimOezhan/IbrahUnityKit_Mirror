using System;
using UnityEngine;

namespace IbrahKit.Settings
{
    [Serializable]
    public abstract class Setting<TValue> : Setting_Base
    {
        [SerializeField] private string key;

        [SerializeField] protected TValue defaultValue;

        protected TValue currentValue;

        public abstract bool TrySetValue(TValue value);

        public TValue GetCurrentValue()
        {
            return currentValue;
        }
    }
}