#region

using System;
using IbrahKit.Debugging;
using IbrahKit.Manager;
using UnityEngine;

#endregion

namespace IbrahKit.Unlockables.Achievements
{
    public class Achievement_Manager : MonoBehaviourSingletonDontDestroyOnLoad<Achievement_Manager>
    {
        private const string PREFIX = "achievement_";

        public static Action<string, bool> OnAchievementUnlocked;

        [SerializeField] private Achievement_Config achievementConfig;

        public void Unlock(Achievement achievement)
        {
            Unlock(achievement.GetKey());
        }

        public void Unlock(string key)
        {
            if (achievementConfig.TryGet(key, out Achievement result))
            {
                bool unlocked = result.IsUnlocked();

                OnAchievementUnlocked?.Invoke(key, unlocked);

                result.Unlock();
            }
            else
            {
                IbrahDebug.LogWarning($"Achievement with key {key} not found");
            }
        }
    }
}