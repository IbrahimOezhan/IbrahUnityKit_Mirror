using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "NewAchievement", menuName = "IbrahKit/Achievement")]
    public class Achievement : Unlockable
    {
        [SerializeField, PreviewField] private Sprite sprite;
        [SerializeField] private bool secret;

        /// <summary>
        /// Returns the data to be displayed. A sprite, a name and a description
        /// </summary>
        /// <param name="secretLoca">The local key incase the achievement is secret</param>
        /// <param name="secretSprite">The sprite incase the achievement is secret</param>
        /// <returns>A sprite, a name and a description</returns>
        public (Sprite, string, string) GetData(string secretLoca, Sprite secretSprite)
        {
            Achievement_JsonData jsonData = GetJson(secret ? secretLoca : key);

            Sprite sprite = secret ? secretSprite : this.sprite;

            sprite = IsUnlocked() ? sprite : Image_Utilities.GrayscaleSprite(sprite);

            return (sprite, jsonData.Title(), jsonData.Description());
        }

        private Achievement_JsonData GetJson(string json)
        {
            if (Json_Utilities.TryDeserialize(json, out Achievement_JsonData res))
            {
                return res;
            }

            return new();
        }
    }
}