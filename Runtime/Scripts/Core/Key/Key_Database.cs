using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "Database", menuName = "Database")]
    public class Key_Database : ScriptableObject
    {
        [OdinSerialize] private Dictionary<string, List<string>> keyValuePairs = new();

        public Dictionary<string, List<string>> Get() => keyValuePairs;
    }
}