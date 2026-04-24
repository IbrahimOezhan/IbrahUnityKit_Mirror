#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Debugging;

#endregion

namespace IbrahKit.Utilities
{
    public class Type_Utilities
    {
        private static bool InheritsFromGeneric(Type type, Type genericBase)
        {
            if (genericBase.IsInterface)
                return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericBase);

            while (type != null && type != typeof(object))
            {
                Type cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;

                if (cur == genericBase) return true;

                type = type.BaseType;
            }

            return false;
        }

        public static Type GetTypeFullName(string fullName)
        {
            Type getType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch
                    {
                        return Array.Empty<Type>();
                    }
                })
                .FirstOrDefault(t => t.FullName == fullName);

            return getType;
        }

        public static IEnumerable<Type> CollectionToTypes(IEnumerable<object> collection)
        {
            return collection.Select(x => x.GetType());
        }

        public static IEnumerable<Type> GetSubTypes(Type baseType, IEnumerable<Type> except = null)
        {
            if (baseType == null)
            {
                IbrahDebug.LogWarning("Base type is null");

                return Array.Empty<Type>();
            }

            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
            {
                try
                {
                    return a.GetTypes();
                }
                catch
                {
                    return Array.Empty<Type>();
                }
            }).Where(t => t.IsClass && !t.IsAbstract && InheritsFromGeneric(t, baseType));

            if (except != null) types = types.Except(except);

            return types;
        }

        public static IEnumerable<string> GetSubTypesAsDropdown(Type baseType, IEnumerable<Type> except = null)
        {
            List<string> subtypes = GetSubTypes(baseType, except).Select(x => x.FullName).ToList();

            subtypes.Sort((a, b) => { return a.CompareTo(b); });

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