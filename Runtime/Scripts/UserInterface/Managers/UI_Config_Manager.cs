using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Config_Manager : Manager_DDOL<UI_Config_Manager>
    {
        [SerializeField] private UI_Audio_SO defaultAudio;

        [SerializeField] private UI_Fitter_Config_SO defaultFitterConfig;

        [SerializeField] private UI_Menu_Config_SO defaultMenuConfig;

        [SerializeField] private UI_Styling_Config_SO defaultStylingConfig;

        [SerializeField] private UI_Layout_Config_SO layoutConfig;

        public UI_Audio_SO GetAudioConfig(UI_Audio_SO overrideValue)
        {
            if (overrideValue != null)
            {
                return overrideValue;
            }

            if (defaultAudio != null)
            {
                return defaultAudio;
            }

            Debug.LogWarning("No source defined");

            return null;
        }

        public UI_Fitter_Config_SO GetFitterConfig(UI_Fitter_Config_SO overrideValue)
        {
            if (overrideValue != null)
            {
                return overrideValue;
            }

            if (defaultFitterConfig != null)
            {
                return defaultFitterConfig;
            }

            Debug.LogWarning("No source defined");

            return null;
        }

        public UI_Menu_Config_SO GetMenuConfig(UI_Menu_Config_SO overrideValue)
        {
            if (overrideValue != null)
            {
                return overrideValue;
            }

            if (defaultMenuConfig != null)
            {
                return defaultMenuConfig;
            }

            Debug.LogWarning("No source defined");

            return null;
        }

        public UI_Styling_Config_SO GetStyleConfig(UI_Styling_Config_SO overrideValue)
        {
            if (overrideValue != null)
            {
                return overrideValue;
            }

            if (defaultStylingConfig != null)
            {
                return defaultStylingConfig;
            }

            Debug.LogWarning("No source defined");

            return null;
        }

        public bool ShowLayout(List<string> layouts)
        {
            return GetActiveLayouts().Intersect(layouts).Count() > 0;
        }

        private List<string> GetActiveLayouts() => layoutConfig.GetActiveLayouts();
    }
}