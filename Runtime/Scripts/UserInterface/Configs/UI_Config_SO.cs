using UnityEngine;

namespace IbrahKit
{
    public class UI_Config_SO<TConfig> : UI_Config_SO_Base where TConfig : UI_Config
    {
        [SerializeReference] private TConfig config;

        public TConfig GetConfig() => config;
    }
}