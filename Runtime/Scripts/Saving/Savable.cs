using System;
using System.Text.Json.Serialization;

namespace IbrahKit.Save
{
    public class Savable
    {
        [JsonInclude] private string fullName;

        public Savable()
        {
            Type ty = this.GetType();

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