#region

using IbrahKit.Keys;

#endregion

public class SysLanguage_Key : Key_Reference<SysLanguage_Key, SysLanguage_Table>
{
    public static implicit operator SysLanguage_Key(string value)
    {
        return new SysLanguage_Key { key = value };
    }

    private class KeyProcessor : Key_Processor<SysLanguage_Key, SysLanguage_Table>
    {
    }
}