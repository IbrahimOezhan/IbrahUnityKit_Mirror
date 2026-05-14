#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Keys
{
    /// <summary>
    /// Base class that holds the key
    /// </summary>
    [Serializable, InlineProperty, HideLabel]
    public partial class Key_Reference : IKey
    {
        [SerializeField] protected string key;

        public string Value => key;

        public string GetKey()
        {
            return key;
        }

        public override string ToString()
        {
            return key;
        }

        public static implicit operator string(Key_Reference reference)
        {
            return reference?.key;
        }

        public static implicit operator Key_Reference(string value)
        {
            return new Key_Reference { key = value };
        }
    }
}