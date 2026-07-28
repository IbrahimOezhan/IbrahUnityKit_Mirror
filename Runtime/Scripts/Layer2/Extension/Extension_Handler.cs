#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Debugging;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

#endregion

namespace IbrahKit.Extension
{
    /// <inheritdoc />
    [Serializable]
    public class Extension_Handler<TExtension> : Extension_Handler_Base where TExtension : Extension
    {
        private const string NONE = "None";

        [SerializeField, OnValueChanged(nameof(AddExtension)), ValueDropdown(nameof(GetDropdown))]
        private string extension = NONE;

        [OdinSerialize, SerializeReference, ListDrawerSettings(HideAddButton = true), DisableContextMenu]
        private List<Extension> extensions = new();

        private IEnumerable GetDropdown()
        {
            return Type_Utilities.GetSubTypesAsString(typeof(TExtension),
                extensions.Where(x => x != null).Select(x => x.GetType()));
        }

        public void AddExtension()
        {
            SortList();

            IEnumerable<Type> types = Type_Utilities.GetSubTypes(typeof(TExtension));

            foreach (Type type in types)
            {
                if (type.FullName != extension) continue;

                object extensionObject = Activator.CreateInstance(type, new object[] { this });

                if (extensionObject == null)
                {
                    IbrahDebug.LogError("Created instance is null");

                    continue;
                }

                if (extensionObject is not TExtension extensionCasted)
                {
                    IbrahDebug.LogError("Object is not of correct type");

                    continue;
                }

                AddExtension2(extensionCasted);

                break;
            }

            extension = NONE;
        }

        public void AddExtension2(TExtension extension)
        {
            extensions.Add(extension);

            SortList();
        }

        private void SortList()
        {
            extensions.RemoveAll(x => x == null);

            extensions.Sort((a, b) => a.GetOrder().CompareTo(b.GetOrder()));
        }

        public bool TryGetExtension<TExtension2>(out TExtension2 result) where TExtension2 : TExtension
        {
            foreach (var item in extensions)
            {
                if (item is TExtension2 ex)
                {
                    result = ex;
                    return true;
                }
            }

            result = null;
            return false;
        }

        public sealed override void RunExtensions() => extensions.ForEach(x => x.Run());

        public sealed override void InitExtensions() => extensions.ForEach(x => x.Init());

        public sealed override void Cleanup() => extensions.ForEach(x => x.Cleanup());

        protected List<Extension> GetExtensions() => extensions;
    }
}