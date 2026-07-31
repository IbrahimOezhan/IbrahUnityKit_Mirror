#region

using IbrahKit.Keys;

#endregion

public class SysLanguage_Key : Key_Reference<SysLanguage_Key, SysLanguageKeyTable>
{
    public static implicit operator SysLanguage_Key(string value)
    {
        return new SysLanguage_Key { key = value };
    }
}