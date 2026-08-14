#region

using IbrahKit.UI;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Core.Config
{
    public abstract class UI_Config<TConfig> : UI_Config_Base where TConfig : UI_Config<TConfig>
    {
        public static bool TryGet(Transform transform, out TConfig config)
        {
            IUIConfigHolder[] t = transform.BetterGetComponentsInParents<IUIConfigHolder>();

            for (var i = 0; i < t.Length; i++)
            {
                if (t[i].TryGetConfig(out config))
                {
                    return true;
                }
            }

            if (UI_Config_Manager.TryGet(out UI_Config_Manager configManager))
            {
                if (configManager.GetManagerData().GetConfigs().TryGet(out config))
                {
                    return true;
                }
            }

            if (UI_Config_Manager_Data.Instance.GetConfigs().TryGet(out config))
            {
                return true;
            }

            return false;
        }
    }
}