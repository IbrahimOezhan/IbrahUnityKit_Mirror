using IbrahKit;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Debug = IbrahKit.Debug;

public static class Save_Utilities
{
    public static (bool, bool) Decrypt(string fileContent, string key, out string result)
    {
        bool tryParse = Parse_Utilties.IsValidJson(fileContent);

        bool decrypted = !tryParse;

        if (!tryParse)
        {
            fileContent = String_Utilities.DecryptEncrypt(fileContent, key);

            Debug.Log("File probably encrypted. Attemping decryption");

            tryParse = Parse_Utilties.IsValidJson(fileContent);

            if (!tryParse)
            {
                Debug.LogError("File still not in json format after decryption. Probably damaged");
            }
        }

        result = fileContent;

        return (tryParse, tryParse);
    }

    public static Savable GetSavable(string json)
    {
        JsonSerializerOptions genericOptions = new()
        {
            IncludeFields = true,
            WriteIndented = true,
        };

        return JsonSerializer.Deserialize<Savable>(json, genericOptions);
    }

    public static Type GetSavableType(Savable type)
    {
        return Type.GetType(type.fullName);
    }

    public static Savable GetDerivedSavable(string json, Savable type)
    {
        if (String_Utilities.IsEmpty(json))
        {
            Debug.LogWarning("Passed json is null or empty");
            return null;
        }

        if (type == null)
        {
            Debug.LogWarning("Passed type is null");
            return null;
        }

        JsonSerializerOptions genericOptions = new()
        {
            IncludeFields = true,
            WriteIndented = true,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        };

        Type instanceType = GetSavableType(type);

        return (Savable)JsonSerializer.Deserialize(json, instanceType, genericOptions);
    }
}
