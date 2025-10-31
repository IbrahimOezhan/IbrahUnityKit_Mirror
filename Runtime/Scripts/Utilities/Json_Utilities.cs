using System.Text.Json;

public static class Json_Utilities
{
    public static bool TryDeserialize<T>(string json, out T result) where T : new()
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
        catch
        {
            result = default;

            return false;
        }
    }
}
