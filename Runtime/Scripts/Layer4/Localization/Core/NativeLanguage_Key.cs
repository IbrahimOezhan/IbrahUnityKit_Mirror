#region

using IbrahKit.Keys;

#endregion

public class NativeLanguage_Key : Key_Reference<NativeLanguage_Key, NativeLanguage_Table>
{
    public static implicit operator NativeLanguage_Key(string value)
    {
        return new NativeLanguage_Key { key = value };
    }

    private class KeyProcessor : Key_Processor<NativeLanguage_Key, NativeLanguage_Table>
    {
    }
}