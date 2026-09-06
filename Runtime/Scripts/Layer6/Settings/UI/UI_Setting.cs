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

        public void Init(Setting s)
        {
            setting = s;

            s.onValueChanged += UpdateUI;

            InitPro(s);
        }

        protected abstract bool InitPro(Setting setting);

        public abstract void UpdateUI(Setting setting);

        public Setting GetSetting() => setting;
    }
}