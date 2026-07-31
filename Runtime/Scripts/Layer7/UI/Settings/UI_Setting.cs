#region

using IbrahKit.UI.Generic;
using UnityEngine;

#endregion

namespace IbrahKit.Settings
{
    public abstract class UI_Setting : MonoBehaviour, IUIInit
    {
        private Setting setting;

        public void OnMenuInitBottomUp()
        {
        }

        public void OnMenuInitTopDown()
        {
        }

        public bool CanSpawn(Setting setting)
        {
            return CanSpawnPro(setting);
        }

        public void Init(Setting setting)
        {
            this.setting = setting;

            setting.OnValueChanged += UpdateUI;
        }

        protected abstract bool CanSpawnPro(Setting setting);

        public abstract void UpdateUI();

        public Setting GetSetting() => setting;
    }
}