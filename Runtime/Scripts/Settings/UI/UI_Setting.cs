using IbrahKit.Settings;
using UnityEngine;

public abstract class UI_Setting : MonoBehaviour
{
    private Setting_Base setting;

    public bool TryInit(Setting_Base setting)
    {
        if (TryInitPro(setting))
        {
            this.setting = setting;
            return true;
        }

        return false;
    }

    protected abstract bool TryInitPro(Setting_Base setting);

    public abstract void UpdateUI();

    public Setting_Base GetSetting() => setting;
}
