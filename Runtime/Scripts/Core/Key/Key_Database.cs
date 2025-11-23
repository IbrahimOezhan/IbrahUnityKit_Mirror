using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "Database", menuName = "Database")]
    public class Key_Database : ScriptableObject
    {
        [OdinSerialize] private Dictionary<string, List<string>> keyValuePairs = new();

        public List<string> GetKeys() => keyValuePairs.Keys.ToList();

        public Dictionary<string, List<string>> Get() => keyValuePairs;
    }
}