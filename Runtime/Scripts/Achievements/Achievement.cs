using Sirenix.OdinInspector;
using System.Text.Json;
using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "NewAchievement", menuName = "IbrahKit/Achievement")]
    public class Achievement : Unlockable
    {
        [SerializeField, PreviewField] private Sprite sprite;
        [SerializeField] private bool secret;

        public (Sprite, string, string) GetData(string secretData, Sprite secretSprite)
        {
            Achievement_JsonData data = GetJson(secret ? secretData : key);

            Sprite s = secret ? secretSprite : sprite;

            s = IsUnlocked() ? s : Image_Utilities.GrayscaleSprite(s);

            return (s, data.Title(), data.Description());
        }

        private Achievement_JsonData GetJson(string json)
        {
            JsonSerializerOptions options = new();
            options.IncludeFields = true;

            try
            {
                Achievement_JsonData data = JsonSerializer.Deserialize<Achievement_JsonData>(json, options);
                return data;
            }
            catch
            {
                return new();
            }

        }
    }
}