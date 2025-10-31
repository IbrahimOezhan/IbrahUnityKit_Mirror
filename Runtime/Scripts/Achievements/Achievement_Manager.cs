using System;
using UnityEngine;

namespace IbrahKit
{
    public class Achievement_Manager : Manager_DDOL<Achievement_Manager>
    {
        private const string PREFIX = "achievement_";

        [SerializeField] private Achievement_Config achievementConfig;

        public static Action<string, bool> OnAchievementUnlocked;

        public void Unlock(Achievement achievement)
        {
            Unlock(achievement.GetKey());
        }

        public void Unlock(string key)
        {
            Achievement found = achievementConfig.GetWithKey(key);

            if (found != null)
            {
                bool unlocked = found.IsUnlocked();

                OnAchievementUnlocked?.Invoke(key, unlocked);

                found.Unlock();
            }
            else
            {
                Debug.LogWarning($"Achievement with key {key} not found");
            }
        }
    }
}