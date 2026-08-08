#region

using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;

#endregion

namespace IbrahKit.Localization
{
    [Serializable]
    public class LocalJsonParser : ILocalDataParser
    {
        public override void Parse(string data, Dictionary<string, string[]> dictionary, int languageIndex, int languageCount)
        {
            JsonNode node = JsonNode.Parse(data);

            Populate(node, dictionary, languageIndex,languageCount);
        }

        private void Populate(JsonNode node, Dictionary<string, string[]> data, int languageIndex, int languageCount)
        {
            switch (node)
            {
                case JsonValue value:
                    if (data.TryGetValue(value.GetPath(), out string[] values))
                    {
                        values[languageIndex] =  value.GetValue<string>();
                    }
                    else
                    {

                        string[] arr = new string[languageCount];
                        arr[languageIndex] =  value.GetValue<string>();
                        data.TryAdd(value.GetPath(), arr);
                    }

                    break;
                case JsonObject @object:
                {
                    foreach (var keyValuePair in @object)
                    {
                        Populate(keyValuePair.Value, data, languageIndex,languageCount);
                    }

                    break;
                }
            }
        }
    }
}