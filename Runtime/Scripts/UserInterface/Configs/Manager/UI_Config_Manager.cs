using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit.UI
{
    public class UI_Config_Manager : Manager_Global_Data<UI_Config_Manager, UI_Config_Manager_Data>
    {
        public UI_Configs GetConfigs() => GetManagerData().GetConfigs();

        public T GetConfig<T>(params Override_Config_SO<T>[] configs) where T : ScriptableObject
        {
            for (int i = 0; i < configs.Length; i++)
            {
                if (configs[i].TryGet(out T value))
                {
                    return value;
                }
            }

            return default;
        }

        public bool ShowLayout(UI_Layout_Config layoutConfig, List<string> layouts)
        {
            return layoutConfig.GetActiveLayouts().Intersect(layouts).Count() > 0;
        }
    }
}