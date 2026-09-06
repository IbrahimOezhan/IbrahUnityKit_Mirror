#region

using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR_64
using Sirenix.OdinInspector.Editor;
#endif

#endregion

namespace IbrahKit.Keys
{
    /// <summary>
    ///     Base class that holds the key. Is used on objects that want to get the accosiated value. By adding this as a
    ///     serialized field to the inspector you can choose a key out of corresponding
    /// </summary>
    [Serializable, InlineProperty, HideLabel]
    public abstract class Key_Reference<TKey, TTable> : IKey where TTable : Key_Table<TKey, TTable>
        where TKey : Key_Reference<TKey, TTable>, new()
    {
        [SerializeField] protected string key;

        public string Key => key;

        public string GetKey()
        {
            return key;
        }

        public override string ToString()
        {
            return key;
        }

#if UNITY_EDITOR

        public static List<string> GetDropdownValues()
        {
            try
            {
                return Key_Table<TKey, TTable>.Instance.Values;
            }
            catch
            {
                return new List<string>() { typeof(TKey).Name };
            }
        }

#endif

        public static implicit operator string(Key_Reference<TKey, TTable> reference)
        {
            return reference?.key;
        }
#if UNITY_EDITOR
        /**
         * Handles adding a dropdown to the key member of the key_reference. Must be specialized for each key for it to work
         */
        protected class Key_Processor : OdinAttributeProcessor<Key_Reference<TKey, TTable>>
        {
            public sealed override void ProcessChildMemberAttributes(InspectorProperty parentProperty,
                MemberInfo member, List<Attribute> attributes)
            {
                if (member.Name != "key") return;

                attributes.Add(new LabelTextAttribute(parentProperty.NiceName));

                attributes.Add(new ValueDropdownAttribute(nameof(GetDropdownValues)));
            }
        }
#else
// Dummy class that allows you to not wrap sub classes into #if UNITY_Editor since the base class still exists

        protected class Key_Processor
        {
        }
#endif
    }
}