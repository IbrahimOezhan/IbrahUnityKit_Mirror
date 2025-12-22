using IbrahKit.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace IbrahKit
{
    public class UI_Menu_Controller_Audio : UI_Menu_Controller
    {
        protected override void OnInit()
        {

        }

        public override void Lifecycle()
        {

        }

        public override void OnMenuDisabled()
        {
        }

        public override void OnMenuEnabled()
        {
        }

        public void OnClickAudio()
        {
            if (UI_Configs.TryGet<UI_Audio_Config_Override, UI_Audio_Config_SO, UI_Audio_Config>(UI_Configs.GetConfigs(GetMenu().transform), out UI_Audio_Config result))
            {
                result.OnClick();
            }
        }

        public void OnHoverAudio()
        {
            if (UI_Configs.TryGet<UI_Audio_Config_Override, UI_Audio_Config_SO, UI_Audio_Config>(UI_Configs.GetConfigs(GetMenu().transform), out UI_Audio_Config result))
            {
                result.OnHover();
            }
        }
    }
}
