using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace IbrahKit.Settings
{
    [CreateAssetMenu(fileName = "NewSettingConfig", menuName = "IbrahKit/SettingConfig")]
    public abstract class Setting_Config<TSetting> : ScriptableObject, ISettingConfig, ISelfValidator where TSetting : Setting_Base, new()
    {
        [SerializeField] private string key;

        [SerializeField, ValueDropdown(nameof(GetDropdown))] private string type;

        public abstract string GetDefaultValue();

        public IEnumerable GetDropdown()
        {
            return Type_Utilities.GetAllTypesDropdownFormat(typeof(TSetting));
        }

        public bool TryGetInstance(out Setting_Base result)
        {
            if (Settings_Manager.GetInstance().TryGet(GetKey(), out result)) return true;

            string value = Settings_Manager.GetInstance().GetValue(GetKey(), GetDefaultValue());

            if (float.TryParse(value, out float _))
            {
                result = new TSetting();
                return true;
            }

            result = null;
            return false;
        }

        public string GetKey() => key;

        public void Validate(SelfValidationResult result)
        {
            Setting_Base setting = new TSetting();

            UI_Setting settingTest = (UI_Setting)Activator.CreateInstance(setting.GetType());

            if (!settingTest.CanSpawn(setting))
            {
                result.AddError("UI is not compatible with the setting");
            }
        }
    }
}