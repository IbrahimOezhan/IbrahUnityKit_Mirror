using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IbrahKit
{
    public class Type_Utilities
    {
        public static Type[] GetAllTypes(Type baseType)
        {
            if (baseType == null)
            {
                IbrahDebug.LogWarning("Base type is null");
                return Array.Empty<Type>();
            }

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Array.Empty<Type>(); }
                })
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    InheritsFromGeneric(t, baseType))
                .ToArray();
        }

        private static bool InheritsFromGeneric(Type type, Type genericBase)
        {
            if (genericBase.IsInterface)
                return type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericBase);

            while (type != null && type != typeof(object))
            {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (cur == genericBase)
                    return true;
                type = type.BaseType;
            }
            return false;
        }

        public static IEnumerable GetAllTypesDropdownFormat(Type baseType)
        {
            List<string> subtypes = GetAllTypes(baseType).Select(x => x.Name).ToList();

            subtypes.Sort((a, b) =>
            {
                return a.CompareTo(b);
            });

            if (subtypes.Count > 0)
            {
                subtypes.Insert(0, "None");
            }
            else
            {
                subtypes.Add("None");
            }

            return subtypes;
        }
    }
}
