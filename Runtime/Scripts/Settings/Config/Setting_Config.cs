using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace IbrahKit.Settings
{
    [CreateAssetMenu(fileName = "NewSettingConfig", menuName = "IbrahKit/SettingConfig")]
    public abstract class Setting_Config<TSetting> : ScriptableObject where TSetting : Setting_Base, new()
    {
        [SerializeField] private string key;

        [SerializeField, ValueDropdown(nameof(GetDropdown))] private string type;

        [SerializeField] private UI_Setting setting;

        public abstract string GetDefaultValue();

        public IEnumerable GetDropdown()
        {
            return Type_Utilities.GetAllTypesDropdownFormat(typeof(TSetting));
        }

        //public bool TryCreateAndDisplay<TSettingUI>(TSettingUI ui, out Setting_Base result) where TSettingUI : UI_Setting , new()
        //{
        //    if (TryCreate(out result))
        //    {
        //        TSettingUI test = new();

        //        if (test.CanSpawn(result))
        //        {
        //            return true;
        //        }
        //    }

        //    result = null;
        //    return false;
        //}

        //public bool TryCreate(out Setting_Base result)
        //{
        //    if (Settings_Manager.GetInstance().TryGetValue(GetKey(), GetDefaultValue(), out string value))
        //    {
        //        if (float.TryParse(value, out float floatValue))
        //        {
        //            result = new TSetting();
        //            return true;
        //        }
        //    }

        //    result = null;
        //    return false;
        //}

        //public string GetKey() => key;



        //public void Validate(SelfValidationResult result)
        //{
        //    Setting_Base setting = new TSetting();

        //    UI_Setting settingTest = (UI_Setting)Activator.CreateInstance(setting.GetType());


        //}
    }
}