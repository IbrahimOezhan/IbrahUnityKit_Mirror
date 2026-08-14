#region

using Sirenix.Serialization;

#endregion

namespace IbrahKit.UI.Core.Config
{
    public class UI_Config_Manager_Data : SerializedScriptableObjectSingleton<UI_Config_Manager_Data>
    {
        [OdinSerialize] private UI_Configs configs;

        public UI_Configs GetConfigs() => configs;
    }
}