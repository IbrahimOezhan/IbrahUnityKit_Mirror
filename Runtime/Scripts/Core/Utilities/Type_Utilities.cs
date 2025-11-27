using IbrahKit.Debugging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IbrahKit
{
    public class Type_Utilities
    {
        public static IEnumerable<Type> GetTypesInCollection(IEnumerable<object> collection)
        {
            List<Type> types = new();

            foreach (object item in collection)
            {
                types.Add(item.GetType());
            }

            return types;
        }

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
                .Where(t => t.IsClass && !t.IsAbstract && InheritsFromGeneric(t, baseType)).ToArray();
        }

        private static bool InheritsFromGeneric(Type type, Type genericBase)
        {
            if (genericBase.IsInterface) return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericBase);

            while (type != null && type != typeof(object))
            {
                Type cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;

                if (cur == genericBase) return true;

                type = type.BaseType;
            }

            return false;
        }

        public static IEnumerable GetAllTypesDropdownFormat(Type baseType, IEnumerable<Type> except = null)
        {
            IEnumerable<Type> subtypes = GetAllTypes(baseType).ToList();

            if (except != null) subtypes = subtypes.Except(except);

            List<string> subtypeNames = subtypes.Select(x => x.FullName).ToList();

            subtypeNames.Sort((a, b) =>
            {
                return a.CompareTo(b);
            });

            if (subtypeNames.Count > 0)
            {
                subtypeNames.Insert(0, "None");
            }
            else
            {
                subtypeNames.Add("None");
            }

            return subtypeNames;
        }
    }
}
