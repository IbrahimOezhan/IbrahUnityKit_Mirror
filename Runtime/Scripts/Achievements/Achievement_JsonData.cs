using System.Text.Json.Serialization;

namespace IbrahKit
{
    /// <summary>
    /// Holds the text data for an achivement. Must be specified in the local table
    /// </summary>
    public class Achievement_JsonData
    {
        [JsonInclude] private string title;
        [JsonInclude] private string description;

        public string Title()
        {
            return title;
        }

        public string Description()
        {
            return description;
        }
    }
}