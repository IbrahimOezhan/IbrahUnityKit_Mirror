#region

using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [CreateAssetMenu(fileName = "NewUIAudioConfig", menuName = "IbrahKit/UI_Audio_Config")]
    public abstract class UI_Audio_Config : Config<UI_Audio_Config>
    {
        public abstract void OnClick();

        public abstract void OnHover();
    }
}