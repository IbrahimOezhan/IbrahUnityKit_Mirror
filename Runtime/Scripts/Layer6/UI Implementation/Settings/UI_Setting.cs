#region

using IbrahKit.UI;
using UnityEngine;

#endregion

namespace IbrahKit.Settings
{
    public abstract class UI_Setting : MonoBehaviour, IMenuInit
    {
        private Setting setting;

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

        public abstract void OnMenuInit(UI_Menu menu);
    }
}