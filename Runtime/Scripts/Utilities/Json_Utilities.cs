using System;
using System.Text.Json;

public static class Json_Utilities
{
    public static bool TryDeserialize<T>(string json, out T result, bool throwWarning = true) where T : new()
    {
        JsonSerializerOptions options = new()
        {
            IncludeFields = true
        };

        try
        {
            result = JsonSerializer.Deserialize<T>(json, options);

            return true;
        }
        catch (Exception e)
        {
            if (throwWarning) IbrahKit.Debug.LogWarning(e.Message);

            result = default;

            return false;
        }
    }
}
