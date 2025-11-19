using IbrahKit.Settings;
using UnityEngine;

public abstract class Setting_Config : ScriptableObject
{
    [SerializeField] private string key;

    public abstract bool TryCreateAndDisplay(UI_Setting uiPrefab, out Setting_Base result);

    public abstract bool TryCreate(out Setting_Base result);

    public string GetKey() => key;
}
