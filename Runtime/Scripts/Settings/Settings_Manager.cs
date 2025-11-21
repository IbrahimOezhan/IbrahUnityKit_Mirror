using IbrahKit.Settings;

namespace IbrahKit
{
    public class Settings_Manager : Manager_DDOL<Settings_Manager>
    {
        public void OpenSettings(UI_Menu menu)
        {

        }

        public bool TryGetValue(string key, string defaultValue, out string value)
        {
            value = string.Empty;
            return false;
        }

        public bool TryGet(string key, out Setting_Base setting)
        {
            setting = null;
            return false;
        }
    }
}