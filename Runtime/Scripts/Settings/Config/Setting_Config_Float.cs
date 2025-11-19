using IbrahKit.Settings;
using UnityEngine;

public class Setting_Config_Float : Setting_Config_Number
{
    [SerializeField] private float defaultValue;
    [SerializeField] private Vector2 valueRange;

    public override bool TryCreateAndDisplay(UI_Setting ui, out Setting_Base result)
    {
        if (TryCreate(out result))
        {
            if (ui.TryInit(result))
            {
                return true;
            }
        }

        result = null;
        return false;
    }

    public override bool TryCreate(out Setting_Base result)
    {
        if (Settings_Manager.GetInstance().TryGetValue(GetKey(), defaultValue.ToString(), out string value))
        {
            if (float.TryParse(value, out float floatValue))
            {
                result = new Setting_Float(floatValue);
                return true;
            }
        }

        result = null;
        return false;
    }
}
