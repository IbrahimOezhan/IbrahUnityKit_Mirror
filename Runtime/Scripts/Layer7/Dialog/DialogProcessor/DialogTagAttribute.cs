using UnityEngine;

namespace IbrahKit.Dialog
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct,
        AllowMultiple = true)]
    public class DialogTagAttribute : System.Attribute
    {
        string Name;

        public DialogTagAttribute(string name)
        {
            Name = name;
        }

        public string GetName() => Name;
    }

}

