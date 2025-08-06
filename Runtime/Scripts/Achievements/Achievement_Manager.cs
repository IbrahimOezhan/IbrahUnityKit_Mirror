using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace IbrahKit
{
    public class Achievement_Manager : MonoBehaviour
    {
        private const string prefix = "achievement_";

        [SerializeField] private List<Achievement> achievements = new();

        [SerializeField, Dropdown(Local_Manager.DROP)] private string secretString;

        [SerializeField] private Sprite secretSprite;

        public static Action<string,bool> OnAchievementUnlocked;

        public static Achievement_Manager Instance;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Unlock(Achievement achievement)
        {
            Unlock(achievement.GetKey());
        }

        public void Unlock(string key)
        {
            string localKey = (prefix + key.ToLower());

            Achievement found = achievements.Find(x => x.GetKey() == localKey);

            if(found != null)
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