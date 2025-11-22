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

        protected override bool CanSpawnPro(Setting_Base setting)
        {
            if (setting is not ISettingNumber num)
            {
                return false;
            }

            if (left == null)
            {
                return false;
            }

            if (right == null)
            {
                return false;
            }

            left.GetStateController().GetOnPressSuccess().AddListener(num.Increment);
            right.GetStateController().GetOnPressSuccess().AddListener(num.Decrement);

            return true;
        }

        public override void UpdateUI()
        {
            value.SetText(GetSetting().GetValue());
        }

        public override void OnMenuInit(UI_Menu menu)
        {
            throw new System.NotImplementedException();
        }
    }
}