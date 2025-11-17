using System;

namespace IbrahKit
{
    public class OverrideStruct<T> where T : struct
    {
        private readonly T baseValue;
        private Nullable<T> overrideValue = null;

        public OverrideStruct(T baseValue)
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