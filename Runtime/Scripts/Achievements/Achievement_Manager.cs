using System;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    public class Achievement_Manager : Manager_Base<Achievement_Manager>
    {
        private const string prefix = "achievement_";

        [SerializeField] private List<Achievement> achievements = new();

        [SerializeField, Dropdown(Local_Manager.DROP)] private string secretString;

        [SerializeField] private Sprite secretSprite;

        public static Action<string, bool> OnAchievementUnlocked;

        public void Unlock(Achievement achievement)
        {
            Unlock(achievement.GetKey());
        }

        public void Unlock(string key)
        {
            key = key.Replace(prefix, "");

            string localKey = (prefix + key.ToLower());

            Achievement found = achievements.Find(x => x.GetKey() == localKey);

            if (found != null)
            {
                bool unlocked = found.IsUnlocked();

                OnAchievementUnlocked?.Invoke(key, unlocked);

                found.Unlock();
            }
            else
            {
                Debug.LogWarning($"Achievement with key {localKey} not found");
            }
        }

        public (Sprite, string, string)[] GetAchievements()
        {
            (Sprite, string, string)[] result = new (Sprite, string, string)[achievements.Count];

            for (int i = 0; i < achievements.Count; i++)
            {
                result[i] = achievements[i].GetData(secretString, secretSprite);
            }

            return result;
        }
    }
}