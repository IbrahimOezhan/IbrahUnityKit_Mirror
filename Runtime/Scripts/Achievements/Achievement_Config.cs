using IbrahKit;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementConfig", menuName = "IbrahKit/AchievemntConfig")]
public class Achievement_Config : ScriptableObject
{
    [SerializeField] private List<Achievement> achievements = new();

    [SerializeField, Dropdown(Local_Manager.DROP)] private string secretString;

    [SerializeField] private Sprite secretSprite;

    public List<Achievement> Get()
    {
        return achievements;
    }

    public Achievement GetWithKey(string key)
    {
        return achievements.Find(x => x.GetKey().Equals(key));
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
