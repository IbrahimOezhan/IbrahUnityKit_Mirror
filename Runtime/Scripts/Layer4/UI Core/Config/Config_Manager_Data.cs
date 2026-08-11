#region

using Sirenix.Serialization;

#endregion

public class Config_Manager_Data : SerializedScriptableObjectSingleton<Config_Manager_Data>
{
    [OdinSerialize] private Configs configs;

    public Configs GetConfigs() => configs;
}