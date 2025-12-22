using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit.UI
{
    public class UI_Config_SO<TConfig> : UI_Config_SO_Base where TConfig : UI_Config
    {
        [SerializeField, InlineProperty, HideLabel] private TConfig config;

        public TConfig GetConfig() => config;
    }
}