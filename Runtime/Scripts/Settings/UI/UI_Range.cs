using IbrahKit.Settings;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Range : UI_Setting
    {
        [SerializeField]
        private UI_Selectable left;

        [SerializeField]
        private UI_Selectable right;

        [SerializeField]
        private UI_Interative_Extension_Text_Setter value;

        protected override bool TryInitPro(Setting_Base setting)
        {
            return setting is ISettingNumber;
        }

        public override void UpdateUI()
        {
            value.SetText(GetSetting().GetValue());
        }
    }
}