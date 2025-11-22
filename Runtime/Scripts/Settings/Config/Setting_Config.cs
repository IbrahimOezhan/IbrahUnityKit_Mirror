using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace IbrahKit.Settings
{
    public abstract class Setting_Config<T> : ScriptableObject where T : Setting_Base, new()
    {
        [SerializeField] private string key;

        [SerializeField, ValueDropdown(nameof(GetDropdown))] private string type;

        public IEnumerable GetDropdown()
        {
            return Type_Utilities.GetAllTypesDropdownFormat(typeof(T));

        }

        public bool TryCreateAndDisplay(UI_Setting ui, out Setting_Base result)
        {
            if (TryCreate(out result))
            {
                if (ui.CanSpawn(result))
                {
                    return true;
                }
            }

            result = null;
            return false;
        }

        public bool TryCreate(out Setting_Base result)
        {
            if (Settings_Manager.GetInstance().TryGetValue(GetKey(), GetDefaultValue(), out string value))
            {
                if (float.TryParse(value, out float floatValue))
                {
                    result = new T();
                    return true;
                }
            }

            result = null;
            return false;
        }

        public string GetKey() => key;

        public abstract string GetDefaultValue();
    }
}