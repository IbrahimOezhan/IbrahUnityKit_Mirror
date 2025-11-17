using System;
using UnityEngine;

namespace IbrahKit
{
    [Serializable]
    public abstract class Setting<TValue>
    {
        [SerializeField] protected TValue defaultValue;

        protected TValue currentValue;

        public abstract bool SetValue(TValue value);

        public TValue GetCurrentValue()
        {
            return currentValue;
        }
    }
}