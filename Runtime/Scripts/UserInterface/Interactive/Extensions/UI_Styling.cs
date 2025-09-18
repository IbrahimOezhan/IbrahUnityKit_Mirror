using TMPro;
using UnityEngine;

namespace IbrahKit
{
    public abstract class UI_Styling : UI_Extension
    {
        public UI_Styling_Config GetResolvedStyle(int defaultSize, Color defaultColor)
        {
            UI_Styling_Config defaultStyle = new UI_Styling_Config(

                Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"),

                TMP_Settings.defaultFontAsset, defaultSize, defaultColor
            );

            UI_Styling_Config resolvedStyle = null;

            if (UI_Configs.GetStyle(UI_Configs.GetConfigs(transform), out UI_Styling_Config_SO result))
            {
                resolvedStyle = result.GetStyle();
            }

            resolvedStyle ??= defaultStyle;

            return resolvedStyle;
        }
    }
}
