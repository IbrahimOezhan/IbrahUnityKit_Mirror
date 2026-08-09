using IbrahKit.UI;
using IbrahKit.Utilities;
using UnityEngine;

public abstract class Config<TConfig> : Config_Base where TConfig : Config<TConfig>
{
    public static bool TryGet(Transform transform, out TConfig config)
    {
        IConfigHolder[] t = transform.BetterGetComponentsInParents<IConfigHolder>();

        for (var i = 0; i < t.Length; i++)
        {
            if (t[i].TryGetConfig(out config))
            {
                return true;
            }
        }

        if (Config_Manager.TryGet(out Config_Manager configManager))
        {
            if (configManager.GetManagerData().GetConfigs().TryGet(out config))
            {
                return true;
            }
        }

        if (Config_Manager_Data.Instance.GetConfigs().TryGet(out config))
        {
            return true;
        }
        
        return false;
    }
}
