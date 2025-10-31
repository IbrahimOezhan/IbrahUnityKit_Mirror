using System;

namespace IbrahKit
{
    public class OverrideValue<T> where T : struct
    {
        private T baseValue;
        private Nullable<T> overrideValue = null;

        public OverrideValue(T baseValue)
        {
            this.baseValue = baseValue;
        }

        public void SetOverride(T overrideBaseSpeed)
        {
            this.overrideValue = overrideBaseSpeed;
        }

        public void ClearOverride()
        {
            this.overrideValue = null;
        }

        public T GetValue()
        {
            return overrideValue != null ? overrideValue.Value : baseValue;
        }
    }
}