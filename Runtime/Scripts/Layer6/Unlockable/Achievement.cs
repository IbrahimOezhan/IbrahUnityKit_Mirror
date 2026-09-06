#region

using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Unlockables.Achievements
{
    [CreateAssetMenu(fileName = "NewAchievement", menuName = "IbrahKit/Unlockable/Achievement")]
    public partial class Achievement : Unlockable
    {
        [SerializeField] private bool secret;

        [SerializeField]
#if ODIN_INSPECTOR
        [PreviewField]
#endif
        private Sprite sprite;

        /// <summary>
        ///     Returns the data to be displayed. A sprite, a name and a description
        /// </summary>
        /// <param name="secretLocal">The local key in case the achievement is secret</param>
        /// <param name="secretSprite">The sprite in case the achievement is secret</param>
        /// <returns>A sprite, a name and a description</returns>
        public (Sprite, string, string) GetData(string secretLocal, Sprite secretSprite)
        {
            if (!Json_Utilities.TryDeserialize(secret ? secretLocal : key, out JsonData jsonData))
            {
                jsonData = new();
            }

            Sprite sprite = secret ? secretSprite : this.sprite;

            sprite = IsUnlocked() ? sprite : sprite.texture.Grayscale().ToSprite(sprite.pixelsPerUnit);

            return (sprite, jsonData.Title(), jsonData.Description());
        }
    }
}