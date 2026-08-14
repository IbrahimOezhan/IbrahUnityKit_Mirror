#region

using IbrahKit.UI.Core.Config;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [CreateAssetMenu(fileName = "NewUIAudioConfig", menuName = "IbrahKit/UI/Selectable/AudioConfig")]
    public abstract class UI_Audio_Config : UI_Config<UI_Audio_Config>
    {
        public abstract void OnClick();

        public abstract void OnHover();
    }
}