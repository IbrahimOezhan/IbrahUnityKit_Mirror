#region

using IbrahKit.Settings;
using IbrahKit.UI;
using IbrahKit.UI.Selectable;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit
{
    public class UI_Setting_Range : UI_Setting
    {
        [SerializeField, Required] private UI_Selectable left;

        [SerializeField, Required] private UI_Selectable right;

        [SerializeField] private UI_Modifier_Text_Modifier value;

        protected override bool InitPro(Setting setting)
        {
            left.GetStateController().GetOnPressSuccess().AddListener(setting.Increment);

            right.GetStateController().GetOnPressSuccess().AddListener(setting.Decrement);

            return true;
        }

        public override void UpdateUI()
        {
            value.GetStaticSetter().SetText(GetSetting().GetValue());
        }
    }
}