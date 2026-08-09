using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;

[Serializable]
public class Configs 
{
    [OdinSerialize] private HashSet<Config_Base> configs = new();

    public bool TryGet<TConfig>(out TConfig config) where TConfig : Config_Base
    {
        config = null;
        
        if (configs.Count(x => x != null) == 0) return false;
        
        Config_Base conf = configs.First(x => x.GetType() == typeof(TConfig));

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
