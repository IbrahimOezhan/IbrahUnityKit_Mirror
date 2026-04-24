#region

using System;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public abstract class UI_Audio_Config : UI_Config
    {
        public abstract void OnClick();

        public abstract void OnHover();
    }
}