#region

using System.Collections.Generic;
using IbrahKit.Settings;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Setting_Config : Config<UI_Setting_Config>
    {
        [SerializeField] private List<UI_Setting> settings = new();

        public List<UI_Setting> Settings => settings;
    }
}