using IbrahKit.Settings;
using UnityEngine;

[System.Serializable]
public class UI_Setting_Map_Element
{
    [SerializeField] private UI_Setting setting;

    [SerializeField] private ISettingConfig config;

    public bool TryCreateUserInterface(Vector3 positon, Quaternion rotation, Transform parent, out UI_Setting result)
    {
        if (config.TryGetInstance(out Setting_Base settingResult))
        {
            result = Object.Instantiate(setting, positon, rotation);

            result.transform.parent = parent;

            result.Init(settingResult);

            return true;
        }

        result = null;

        return false;
    }
}
