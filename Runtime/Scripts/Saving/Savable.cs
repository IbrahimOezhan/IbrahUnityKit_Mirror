using System;

using System.Text.Json.Serialization;

namespace IbrahKit.Save
{
    public class Savable
    {
        [JsonInclude][System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Cannot be used for attributes that get deserialized")]
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