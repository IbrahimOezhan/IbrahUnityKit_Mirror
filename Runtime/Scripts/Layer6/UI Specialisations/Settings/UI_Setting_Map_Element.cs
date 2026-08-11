#region

using System;
using IbrahKit.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

[Serializable]
public class UI_Setting_Map_Element
{
    [SerializeField] private Setting_Config setting;

    [SerializeField]
    private UI_Setting uiSetting;

    public Setting_Config GetSettingConfig() => setting;
    public UI_Setting UiSetting => uiSetting;
}