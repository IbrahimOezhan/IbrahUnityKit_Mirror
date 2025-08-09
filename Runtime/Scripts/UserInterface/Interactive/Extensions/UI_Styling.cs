using TMPro;
using UnityEngine;

namespace IbrahKit
{
    public abstract class UI_Styling : UI_Extension
    {
        [SerializeField] private UI_Styling_Config_SO customStyle;

        public UI_Styling_Config GetResolvedStyle(int defaultSize, Color defaultColor)
        {
            UI_Styling_Config defaultStyle = new UI_Styling_Config(
                Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"),
                TMP_Settings.defaultFontAsset, defaultSize, defaultColor
            );

            UI_Styling_Config resolvedStyle = customStyle.GetStyle();

            if (UI_Config_Manager.TryGet(out UI_Config_Manager config))
            {
                resolvedStyle = config.GetStyleConfig(customStyle).GetStyle();
            }

            resolvedStyle = resolvedStyle == null ? defaultStyle : resolvedStyle;

            return resolvedStyle;
        }
    }
}
