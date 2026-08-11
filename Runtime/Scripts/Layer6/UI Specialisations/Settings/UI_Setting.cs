#region

using IbrahKit.UI.Generic;
using UnityEngine;

#endregion

namespace IbrahKit.Settings
{
    public abstract class UI_Setting : MonoBehaviour, IUIInit
    {
        private Setting setting;

        private void OnDestroy()
        {
            if (setting != null)
            {
                setting.onValueChanged -= UpdateUI;
            }
        }

        public void OnMenuInitBottomUp()
        {
        }

        public void OnMenuInitTopDown()
        {
        }

        public void Init(Setting setting)
        {
            this.setting = setting;

            setting.onValueChanged += UpdateUI;

            InitPro(setting);
        }

        protected abstract bool InitPro(Setting setting);

        public abstract void UpdateUI();

        public Setting GetSetting() => setting;
    }
}