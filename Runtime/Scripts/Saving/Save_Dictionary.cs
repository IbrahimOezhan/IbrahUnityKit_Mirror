using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IbrahKit.Save
{
    [Serializable]
    public class Save_Dictionary : Savable
    {
        [JsonInclude]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        private Dictionary<string, string> values = new();

        public void Set<T>(string key, T value)
        {
            values[key] = value.ToString();
        }

        public bool TryGet<T>(string key, out T result)
        {
            result = default;

            if (!values.TryGetValue(key, out var tmpResult)) return false;

            try
            {
                result = (T)Convert.ChangeType(tmpResult, typeof(T));

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}