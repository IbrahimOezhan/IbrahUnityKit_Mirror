using System;

namespace IbrahKit.Settings
{
    [Serializable]
    public abstract class Setting<TValue> : Setting_Base
    {
        protected TValue currentValue;

        public abstract bool TrySetValue(TValue value);

        public TValue GetCurrentValue()
        {
            return currentValue;
        }
    }
}