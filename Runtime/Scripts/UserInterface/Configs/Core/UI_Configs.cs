using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class UI_Configs
    {
        [SerializeReference, TypeFilter(nameof(Filter))] private List<Override_Config_SO_Base> overrides = new();

        private IEnumerable<Type> Filter()
        {
            return Type_Utilities.GetSubTypes(typeof(Override_Config_SO_Base)).Except(Type_Utilities.GetTypesInCollection(overrides)).Where(x => !x.ContainsGenericParameters);
        }

        public bool TryGet<TOverrideSO, TConfigSO, TConfig>(out TConfig result)
            where TConfig : UI_Config where TConfigSO : UI_Config_SO<TConfig> where TOverrideSO : Override_Config_SO<TConfigSO>
        {
            foreach (var item in overrides)
            {
                if (item is TOverrideSO so)
                {
                    TConfigSO configSO = so.Get();
                    result = configSO.GetConfig();
                    return result != null;
                }
            }

            result = default;
            return false;
        }


        public static UI_Configs[] GetConfigs(Transform t)
        {
            IConfigHolder[] iConfigs = t.BetterGetComponentsInParents<IConfigHolder>(true);

            bool found = UI_Config_Manager.TryGet(out UI_Config_Manager result);

            UI_Configs[] uiConfigs = new UI_Configs[iConfigs.Length + (found ? 1 : 0)];

            for (int i = 0; i < iConfigs.Length; i++)
            {
                uiConfigs[i] = iConfigs[i].GetConfigs();
            }

            if (found)
            {
                uiConfigs[^1] = result.GetConfigs();
            }

            return uiConfigs;
        }

        public static bool TryGet<TOverrideSO, TConfigSO, TConfig>(UI_Configs[] configs, out TConfig result)
            where TConfig : UI_Config where TConfigSO : UI_Config_SO<TConfig> where TOverrideSO : Override_Config_SO<TConfigSO>
        {
            result = null;
            for (int i = 0; i < configs.Length; i++)
                if (configs[i].TryGet<TOverrideSO, TConfigSO, TConfig>(out result))
                    return true;
            return false;
        }
    }
}