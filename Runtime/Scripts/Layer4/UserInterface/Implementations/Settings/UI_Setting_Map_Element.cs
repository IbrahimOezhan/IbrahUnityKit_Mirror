#region

using System;
using IbrahKit.Settings;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

[Serializable]
public class UI_Setting_Map_Element
{
    [SerializeField] private UI_Setting settingsPrefab;

    [SerializeField] private ISettingConfig config;

    /// <summary>
    /// Creates a dummy setting and dummy ui to check if they are compatible
    /// and if so creates the real setting and ui and connects them
    /// </summary>
    /// <param name="positon"></param> The pos at which to spawn
    /// <param name="rotation"></param> The rot at which to spawn
    /// <param name="parent"></param> The parent at which to spawn
    /// <param name="result"></param> The resulting ui setting instance
    /// <returns></returns> Whether the UI was created
    public bool TryCreateUserInterface(Vector3 positon, Quaternion rotation, Transform parent, out UI_Setting result)
    {
        Setting setting = config.GetDummy();

        UI_Setting settingTest = (UI_Setting)Activator.CreateInstance(setting.GetType());

        if (!settingTest.CanSpawn(setting))
        {
            result = null;

            return false;
        }

        if (!config.TryGetInstance(out Setting settingResult))
        {
            result = null;

            return false;
        }

        result = Object.Instantiate(settingsPrefab, positon, rotation);

        result.transform.parent = parent;

        result.Init(settingResult);

        return true;
    }
}