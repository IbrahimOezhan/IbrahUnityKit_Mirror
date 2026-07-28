#region

using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

#endregion

namespace IbrahKit.Keys
{
    /// <summary>
    ///     Class that holds a dictionary that consists of key value pairs with the value being a string list
    /// </summary>
    [CreateAssetMenu(fileName = "NewKeyDatabase", menuName = "IbrahKit/Key_Database")]
    public class Key_Database : SerializedScriptableObject
    {
        [OdinSerialize, ReadOnly, InlineProperty]
        private Dictionary<string, List<string>> keyValuePairs = new();

        public List<string> GetKeys() => keyValuePairs.Keys.ToList();

        public Dictionary<string, List<string>> GetTables() => keyValuePairs;
    }
}