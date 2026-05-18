#region

using System;

#endregion

namespace IbrahKit.Settings
{
    public class Setting_Float : Setting_Number<float>
    {
        public override void Decrement()
        {
            throw new NotImplementedException();
        }

        public override void Increment()
        {
            throw new NotImplementedException();
        }

        public override bool IsMax()
        {
            throw new NotImplementedException();
        }

        public override bool IsMin()
        {
            throw new NotImplementedException();
        }

        public override bool TrySetValue(string value)
        {
            if (!float.TryParse(value, out float f))
            {
                return false;
            }

            currentValue = f;

            return true;
        }

        public override bool TrySetValue(float value)
        {
            throw new NotImplementedException();
        }
    }
}