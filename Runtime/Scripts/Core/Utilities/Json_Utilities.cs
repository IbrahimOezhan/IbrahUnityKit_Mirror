using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IbrahKit
{
    public static class Json_Utilities
    {
        public static bool TryDeserialize<T>(string json, out T result) where T : new()
        {
            bool resultTry = TryDeserialize(json, typeof(T), out object resultOb);

            if (!resultTry)
            {
                result = default;

                return false;
            }

            result = (T)resultOb;

            return resultTry;
        }

        public static bool TryDeserialize(string json,Type t, out object result)
        {
            try
            {
                result = Deserialize(json,t, JsonUnmappedMemberHandling.Disallow);

                return true;
            }
            catch (Exception e)
            {
                IbrahDebug.LogWarning(e.Message);

                result = default;

                return false;
            }
        }

        public static T Deserialize<T>(string json, JsonUnmappedMemberHandling unmappedMemberHandling = JsonUnmappedMemberHandling.Skip) where T : new()
        {
            return (T) Deserialize(json, typeof(T), unmappedMemberHandling);
        }

        public static object Deserialize(string json, Type t, JsonUnmappedMemberHandling unmappedMemberHandling = JsonUnmappedMemberHandling.Skip)
        {
            JsonSerializerOptions options = new()
            {
                IncludeFields = true,
                WriteIndented = true,
                UnmappedMemberHandling = unmappedMemberHandling,
            };

            return (object)JsonSerializer.Deserialize(json, t, options);
        }
    }
}