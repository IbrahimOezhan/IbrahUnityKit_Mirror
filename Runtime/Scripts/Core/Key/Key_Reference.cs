using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable, InlineProperty, HideLabel]
    public class Key_Reference
    {
        [SerializeField]
        protected string key;

        public string Value => key;

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