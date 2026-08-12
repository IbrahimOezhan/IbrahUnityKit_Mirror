#region

using System.Text.Json.Serialization;

#endregion

namespace IbrahKit.Unlockables.Achievements
{
    public partial class Achievement
    {
        private class JsonData
        {
            [JsonInclude] private string description;

            [JsonInclude] private string title;

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