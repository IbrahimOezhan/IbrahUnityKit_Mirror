using UnityEngine;

namespace IbrahKit.Settings
{
    public abstract class UI_Setting : MonoBehaviour, IMenuUpdate
    {
        private Setting_Base setting;

        public bool CanSpawn(Setting_Base setting)
        {
            return CanSpawnPro(setting);
        }

        public void Init(Setting_Base setting)
        {
            this.setting = setting;

            setting.OnValueChanged += UpdateUI;
        }

        protected abstract bool CanSpawnPro(Setting_Base setting);

        public abstract void UpdateUI();

        public Setting_Base GetSetting() => setting;

        public abstract void OnMenuInit(UI_Menu menu);
    }
}