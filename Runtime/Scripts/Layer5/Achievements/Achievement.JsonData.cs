using System.Text.Json.Serialization;

namespace IbrahKit.Unlockables.Achievements
{
    public partial class Achievement
    {
        private class JsonData
        {
            [JsonInclude]
            private string title;

            [JsonInclude]
            private string description;

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
}