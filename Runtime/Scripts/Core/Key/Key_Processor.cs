using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace IbrahKit
{
    public partial class Key_Reference
    {
        protected abstract class Key_Processor<TValue> : OdinAttributeProcessor<TValue> where TValue : Key_Reference
        {
            public sealed override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
            {
                if (member.Name == "key")
                {
                    attributes.Add(new LabelTextAttribute(parentProperty.NiceName));
                    attributes.Add(new ValueDropdownAttribute($"@Key_Database_Finder.GetKeys(\"{GetDBName()}\")"));
                }
            }

            public abstract string GetDBName();
        }
    }
}