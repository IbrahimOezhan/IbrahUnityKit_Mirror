#region

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

#endregion

namespace IbrahKit.Save
{
    [Serializable]
    public class Savable
    {
        [JsonInclude,SuppressMessage("Style", "IDE0044:Add readonly modifier",
             Justification = "Cannot be used for attributes that get deserialized")]
        private string fullName;

        public Savable()
        {
            Type ty = GetType();

            string assemblyName = ty.Assembly.GetName().Name;

            string qualifiedName = $"{ty.FullName}, {assemblyName}";

            fullName = qualifiedName;
        }

        public Type GetSavableType()
        {
            return Type.GetType(fullName);
        }
    }
}