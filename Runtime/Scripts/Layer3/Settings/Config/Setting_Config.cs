#region

using System.Collections;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Settings
{
    /// <summary>
    /// Contains information on the type of the setting and the key it uses
    /// </summary>
    /// <typeparam name="TSetting"></typeparam> The parent class of which the type can be
    public abstract class Setting_Config<TSetting> : ScriptableObject, ISettingConfig where TSetting : Setting, new()
    {
        [SerializeField] private string key;

        [SerializeField, ValueDropdown(nameof(GetDropdown))]
        private string settingType;

        public bool TryGetInstance(out Setting result)
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

        public abstract string GetDefaultValue();

        public string GetKey() => key;

        public IEnumerable GetDropdown() => Type_Utilities.GetSubTypesAsDropdown(typeof(TSetting));

        public Setting GetDummy()
        {
            return new TSetting();
        }
    }
}