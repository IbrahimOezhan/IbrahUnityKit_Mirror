using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Config_Manager : Manager_DDOL<UI_Config_Manager>
    {
        [SerializeField] private UI_Configs configs;

        public UI_Configs GetConfigs() => configs;

        public T GetConfig<T>(params BoolOverride<T>[] configs)
        {
            for (int i = 0; i < configs.Length; i++)
            {
                if (configs[i].TryGet(out T value))
                {
                    return value;
                }
            }

            return default(T);
        }

        public bool ShowLayout(UI_Layout_Config_SO layoutConfig,List<string> layouts)
        {
            return GetActiveLayouts(layoutConfig).Intersect(layouts).Count() > 0;
        }

        private List<string> GetActiveLayouts(UI_Layout_Config_SO layoutConfig) => layoutConfig.GetActiveLayouts();
    }
}