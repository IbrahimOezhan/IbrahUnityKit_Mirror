#region

using System;

#endregion

namespace IbrahKit.Settings
{
    [Serializable]
    public abstract class Setting<TValue> : Setting
    {
        protected TValue currentValue;

        public abstract bool TrySetValue(string value);

        public abstract bool TrySetValue(TValue value);

        public TValue GetCurrentValue()
        {
            return currentValue;
        }

        public override string GetValue()
        {
            return currentValue.ToString();
        }
    }
}