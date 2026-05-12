#region

using System;
using IbrahKit.Settings;
using IbrahKit.UI;
using UnityEngine;

#endregion

namespace IbrahKit
{
    public class UI_Setting_Range : UI_Setting
    {
        private UI_Interative_Extension_Text_Setter textSetter;

        [SerializeField] private UI_Selectable left;

        [SerializeField] private UI_Selectable right;

        [SerializeField] private UI_Interactive value;

        protected override bool CanSpawnPro(Setting setting)
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

            if (!value.TryGetExtension(out textSetter))
            {
                return false;
            }

            return true;
        }

        public override void UpdateUI()
        {
            textSetter.SetText(GetSetting().GetValue());
        }

        public override void OnMenuInit(UI_Menu menu)
        {
            throw new NotImplementedException();
        }
    }
}