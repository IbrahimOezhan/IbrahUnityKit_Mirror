#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace IbrahKit.Utilities
{
    /// <summary>
    ///     Static class providing utility methods related to Type
    /// </summary>
    public static class Type_Utilities
    {
        public static Type GetTypeByFullName(string fullName)
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
            if (collection == null)
            {
                Debug.LogError("Collection is null");
                return Type.EmptyTypes;
            }
            
            return collection.Select(x => x.GetType());
        }

        public static IEnumerable<Type> GetSubTypes(Type baseType, IEnumerable<Type> except = null)
        {
            if (baseType == null)
            {
                throw new NullReferenceException("Base type is null");
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
            }).Where(t =>
            {
                Type ty = t;
                
                if (ty.IsAbstract) return false;

                if (!baseType.IsGenericType)
                {
                    return baseType.IsAssignableFrom(ty);
                }
                
                if (baseType.IsInterface)
                    return ty.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseType);

                while (ty != null && ty != typeof(object))
                {
                    Type cur = ty.IsGenericType ? ty.GetGenericTypeDefinition() : ty;

                    if (cur == baseType) return true;

                    ty = ty.BaseType;
                }

                return false;
            }).Except(except ?? Type.EmptyTypes);

            return types;
        }

        public static IEnumerable<string> GetSubTypesAsString(Type baseType, IEnumerable<Type> except = null)
        {
            List<string> subtypes = GetSubTypes(baseType, except).Select(x => x.FullName).ToList();

            subtypes.Sort((a, b) => string.Compare(a, b, StringComparison.Ordinal));

            if (subtypes.Count > 0) subtypes.Insert(0, "None");
            else subtypes.Add("None");

            return subtypes;
        }
    }
}