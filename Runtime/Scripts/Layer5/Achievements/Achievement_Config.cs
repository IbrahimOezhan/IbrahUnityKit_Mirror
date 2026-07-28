#region

using System.Collections.Generic;
using IbrahKit.Localization;
using UnityEngine;

#endregion

namespace IbrahKit.Unlockables.Achievements
{
    [CreateAssetMenu(fileName = "AchievementConfig", menuName = "IbrahKit/AchievemntConfig")]
    public class Achievement_Config : ScriptableObject
    {
        [SerializeField] private Local_Key secretString;

        [SerializeField] private Sprite secretSprite;

        [SerializeField] private List<Achievement> achievements = new();

        public List<Achievement> Get()
        {
            return achievements;
        }

        public bool TryGet(string key, out Achievement result)
        {
            result = achievements.Find(x => x.GetKey().Equals(key));

            return result != null;
        }

        public (Sprite, string, string)[] GetAchievements()
        {
            (Sprite, string, string)[] result = new (Sprite, string, string)[Get().Count];

            for (int i = 0; i < Get().Count; i++)
            {
                result[i] = Get()[i].GetData(secretString, secretSprite);
            }

            return result;
        }
    }
}