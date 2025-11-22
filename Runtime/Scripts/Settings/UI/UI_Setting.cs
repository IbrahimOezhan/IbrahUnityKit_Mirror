using UnityEngine;

namespace IbrahKit.Settings
{
    public abstract class UI_Setting : MonoBehaviour, IMenuUpdate
    {
        private Setting_Base setting;

        public bool CanSpawn(Setting_Base setting)
        {
            if (!CanSpawnPro(setting))
            {
                return false;
            }

            this.setting = setting;

            setting.OnValueChanged += UpdateUI;

            return true;
        }

        protected abstract bool CanSpawnPro(Setting_Base setting);

        public abstract void UpdateUI();

        public Setting_Base GetSetting() => setting;

        public abstract void OnMenuInit(UI_Menu menu);
    }
}