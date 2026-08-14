#region

using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;

#endregion

namespace IbrahKit.UI.Core.Config
{
    [Serializable]
    public class UI_Configs
    {
        [OdinSerialize] private HashSet<UI_Config_Base> configs = new();

        public bool TryGet<TConfig>(out TConfig config) where TConfig : UI_Config_Base
        {
            config = null;

            if (configs.Count(x => x != null) == 0) return false;

            UI_Config_Base conf = configs.First(x => x.GetType() == typeof(TConfig));

            if (conf != null && conf is TConfig conf2)
            {
                config = conf2;
            }
            else
            {
                config = null;
            }

            return config != null;
        }
    }
}