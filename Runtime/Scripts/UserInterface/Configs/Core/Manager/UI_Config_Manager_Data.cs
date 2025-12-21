using IbrahKit.UI;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Config_Manager_Data : ScriptableObject
    {
        [SerializeField] private UI_Configs configs;

        public UI_Configs GetConfigs() => configs;
    }
}
