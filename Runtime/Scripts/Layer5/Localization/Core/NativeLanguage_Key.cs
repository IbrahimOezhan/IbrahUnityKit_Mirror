#region

using IbrahKit.Keys;

#endregion

public class NativeLanguage_Key : Key_Reference<NativeLanguage_Key, NativeLanguageKeyTable>
{
    public static implicit operator NativeLanguage_Key(string value)
    {
        return new NativeLanguage_Key { key = value };
    }
}