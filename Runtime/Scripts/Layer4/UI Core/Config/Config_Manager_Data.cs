using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;

public class Config_Manager_Data : SerializedScriptableObjectSingleton<Config_Manager_Data>
{
    [OdinSerialize] private Configs configs;

    public Configs GetConfigs() => configs;
}

[Serializable]
public class HashTypeSet<TType> : HashSet<TType>
{
    public bool Add(TType item)
    {
        return !this.GroupBy(x => x.GetType()).Any(g => g.Count<TType>() > 1) && base.Add(item);
    }
}
