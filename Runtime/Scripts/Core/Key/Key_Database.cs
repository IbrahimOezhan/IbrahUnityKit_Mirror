using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "NewKeyDatabase", menuName = "IbrahKit/Key_Database")]
    public class Key_Database : SerializedScriptableObject
    {
        [OdinSerialize, ReadOnly] private Dictionary<string, List<string>> keyValuePairs = new();

        public List<string> GetKeys() => keyValuePairs.Keys.ToList();

        public Dictionary<string, List<string>> GetPairs() => keyValuePairs;
    }
}