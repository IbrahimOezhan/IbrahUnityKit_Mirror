#region

using IbrahKit.UI;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit
{
    public class UI_Config_Manager_Data : ScriptableObject
    {
        [SerializeField, InlineProperty] private UI_Configs configs;

        public UI_Configs GetConfigs() => configs;
    }
}