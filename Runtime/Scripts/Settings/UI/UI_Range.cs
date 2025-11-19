using IbrahKit.Settings;

public class UI_Range : UI_Setting
{
    public override bool TryInit(Setting_Base setting)
    {
        return setting is ISettingNumber;
    }
}
