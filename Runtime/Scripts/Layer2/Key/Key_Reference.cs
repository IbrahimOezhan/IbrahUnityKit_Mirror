#region

using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

#endregion

namespace IbrahKit.Keys
{
    /// <summary>
    ///     Base class that holds the key. Is used on objects that want to get the accosiated value. By adding this as a
    ///     serialized field to the inspector you can choose a key out of corresponding
    /// </summary>
    [Serializable, InlineProperty, HideLabel]
    public abstract class Key_Reference<TKey, TTable> : IKey where TTable : Table<TKey, TTable>
        where TKey : Key_Reference<TKey, TTable>, new()
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

        public static implicit operator string(Key_Reference<TKey, TTable> reference)
        {
            return reference?.key;
        }

        /**
         * Handles adding a dropdown to the key member of the key_reference. Must be specialized for each key for it to work
         */
        protected abstract class Key_Processor<TTKey, TTTable> : OdinAttributeProcessor<TTKey>
            where TTTable : Table<TTKey, TTTable> where TTKey : Key_Reference<TTKey, TTTable>, new()
        {
            public sealed override void ProcessChildMemberAttributes(InspectorProperty parentProperty,
                MemberInfo member, List<Attribute> attributes)
            {
                if (member.Name != "key") return;

                attributes.Add(new LabelTextAttribute(parentProperty.NiceName));

                attributes.Add(new ValueDropdownAttribute(nameof(Table<TTKey, TTTable>.Instance.Values)));
            }
        }
    }
}