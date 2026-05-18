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
    public abstract class Setting_Config<TSetting> : Setting_Config_Base, ISettingConfig where TSetting : Setting, new()
    {
        [SerializeField, ValueDropdown(nameof(GetDropdown))]
        private string settingType;

        public override bool TryGetInstance(out Setting result)
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

        public IEnumerable GetDropdown() => Type_Utilities.GetSubTypesAsString(typeof(TSetting));

        public override Setting GetDummy()
        {
            return new TSetting();
        }
    }
}