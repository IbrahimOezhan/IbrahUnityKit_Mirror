#region

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using IbrahKit.Debugging;
using IbrahKit.Utilities;

#endregion

namespace IbrahKit.Save
{
    public static class Save_Utilities
    {
        public static Savable Deserialize(string json, Type type, bool throwIfUnmapped)
        {
            if (json.IsEmpty())
            {
                IbrahDebug.LogWarning("Passed json is null or empty");

                return null;
            }

            if (type == null)
            {
                IbrahDebug.LogWarning("Passed type is null");

                return null;
            }

            if (!throwIfUnmapped) return (Savable)Json_Utilities.Deserialize(json, type);

            return (Savable)Json_Utilities.Deserialize(json, type, JsonUnmappedMemberHandling.Disallow);
        }

        public static (Savable, Save_State) DeserializeAndEvaluate(string content, Type t)
        {
            try
            {
                Savable savable = Deserialize(content, t, true);

                return (savable, Save_State.Valid);
            }
            catch (JsonException ex)
            {
                try
                {
                    IbrahDebug.LogException(ex);
                    
                    Savable savable = Deserialize(content, t, false);

                    return (savable, Save_State.Corrupted);
                }
                catch (Exception exx)
                {
                    IbrahDebug.LogException(exx);
                    
                    return (null, Save_State.Corrupted);
                }
            }
            catch(Exception ex)
            {
                IbrahDebug.LogException(ex);
                
                return (null, Save_State.Corrupted);
            }
        }

        public static string GetQualifiedName(Type t)
        {
            string assemblyName = t.Assembly.GetName().Name;

            return $"{t.FullName}, {assemblyName}";
        }
    }
}