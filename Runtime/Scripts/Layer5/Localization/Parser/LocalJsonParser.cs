#region

using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;

#endregion

[Serializable]
public class LocalJsonParser : ILocalDataParser
{
    public void Parse(string data, Dictionary<string, string[]> dictionary, int languageIndex)
    {
        JsonNode node = JsonNode.Parse(data);

        Populate(node, dictionary, languageIndex);
    }

    private void Populate(JsonNode node, Dictionary<string, string[]> data, int languageIndex)
    {
        switch (node)
        {
            case JsonValue value:
                data[value.GetPath()][languageIndex] = value.GetValue<string>();
                break;
            case JsonObject array:
            {
                foreach (var keyValuePair in array)
                {
                    Populate(keyValuePair.Value, data, languageIndex);
                }

                break;
            }
        }
    }
}