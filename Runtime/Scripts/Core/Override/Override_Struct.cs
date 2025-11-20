using System;

namespace IbrahKit
{
    public class Override_Struct<T> : Override_Base<T> where T : struct
    {
        private readonly T baseValue;
        private Nullable<T> overrideValue = null;

        public Override_Struct(T baseValue)
        {
            this.baseValue = baseValue;
        }

        public override void SetOverride(T value)
        {
            this.overrideValue = value;
        }

        public override T GetValue()
        {
            return overrideValue != null ? overrideValue.Value : baseValue;
        }

        public override bool IsOverride()
        {
            return overrideValue != null;
        }

        public override void ClearOverride()
        {
            this.overrideValue = null;

        }
    }
}