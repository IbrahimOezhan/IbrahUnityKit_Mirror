#region

using System;

#endregion

namespace IbrahKit.Settings
{
    [Serializable]
    public abstract class Setting
    {
        public Action OnValueChanged;

        public Setting()
        {
        }

        public abstract bool Init(string initialValue);

        public abstract string GetValue();
    }
}