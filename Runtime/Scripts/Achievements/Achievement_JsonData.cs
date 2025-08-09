using System.Text.Json.Serialization;

namespace IbrahKit
{
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