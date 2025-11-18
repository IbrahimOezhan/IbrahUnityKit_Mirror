using IbrahKit;
using IbrahKit.Settings;
using UnityEngine;

public class Settings_Manager : Manager_DDOL<Settings_Manager>
{
    public bool TryGet(string key, out Setting_Base setting)
    {
        setting = null;
        return false;
    }
}
