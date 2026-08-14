#region

using System.Collections.Generic;
using IbrahKit.Settings;
using IbrahKit.UI.Core.Config;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Setting_Config : UI_Config<UI_Setting_Config>
    {
        [SerializeField] private List<UI_Setting> settings = new();

        public List<UI_Setting> Settings => settings;
    }
}