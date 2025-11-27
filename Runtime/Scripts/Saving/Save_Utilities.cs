using IbrahKit.Debugging;
using System;
using System.Text.Json.Serialization;

namespace IbrahKit.Save
{
    public static class Save_Utilities
    {
        public static bool Decrypt(string fileContent, string key, out string result)
        {
            bool tryParse = Json_Utilities.IsValidJson(fileContent);

            if (!tryParse)
            {
                fileContent = String_Utilities.Encrypt(fileContent, key);

                IbrahDebug.Log("File probably encrypted. Attemping decryption");

                tryParse = Json_Utilities.IsValidJson(fileContent);

                if (!tryParse)
                {
                    IbrahDebug.LogError("File still not in json format after decryption. Probably damaged");
                }
            }

            result = fileContent;

            return tryParse;
        }

        public static Savable GetSavable(string json)
        {
            return Json_Utilities.Deserialize<Savable>(json);
        }

        public static Type GetSavableType(Savable type)
        {
            return Type.GetType(type.fullName);
        }

        public static Savable GetDerivedSavable(string json, Savable type)
        {
            if (String_Utilities.IsEmpty(json))
            {
                IbrahDebug.LogWarning("Passed json is null or empty");
                return null;
            }

            if (type == null)
            {
                IbrahDebug.LogWarning("Passed type is null");
                return null;
            }

            Type instanceType = GetSavableType(type);

            return (Savable)Json_Utilities.Deserialize(json, instanceType, JsonUnmappedMemberHandling.Disallow);
        }
    }

}