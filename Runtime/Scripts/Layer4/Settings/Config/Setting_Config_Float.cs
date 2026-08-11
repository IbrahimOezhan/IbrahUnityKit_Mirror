#region

using UnityEngine;

#endregion

namespace IbrahKit.Settings
{
    [CreateAssetMenu(fileName = "NewSettingConfigNumber", menuName = "IbrahKit/Settings/ConfigNumber")]
    public class Setting_Config_Float : Setting_Config_Number<Setting_Float>
    {
        [SerializeField] private float defaultValue;

        [SerializeField] private Vector2 valueRange;

        public override string GetDefaultValue()
        {
            return defaultValue.ToString();
        }
    }
}