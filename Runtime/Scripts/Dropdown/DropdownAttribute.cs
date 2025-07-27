using System;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class DropdownAttribute : PropertyAttribute
    {
        public string fileName;

        public DropdownAttribute(string fileName)
        {
            this.fileName = fileName;
        }
    }
}
