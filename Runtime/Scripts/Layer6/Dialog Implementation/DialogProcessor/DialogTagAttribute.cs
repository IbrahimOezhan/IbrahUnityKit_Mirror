#region

using System;

#endregion

namespace IbrahKit.Dialog
{
    [AttributeUsage(AttributeTargets.Class |
                    AttributeTargets.Struct,
        AllowMultiple = true)]
    public class DialogTagAttribute : Attribute
    {
        string Name;

        public DialogTagAttribute(string name)
        {
            Name = name;
        }

        public string GetName() => Name;
    }
}