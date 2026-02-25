using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit
{
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
            return Type_Utilities.GetSubTypesAsDropdown(typeof(TExtension), extensions.Select(x => x.GetType()));
        }

        public void AddExtension()
        {
            SortList();

            IEnumerable<Type> types = Type_Utilities.GetSubTypes(typeof(TExtension));

            foreach (Type type in types)
            {
                if (type.FullName != extension) continue;

                AddExtension2((TExtension)Activator.CreateInstance(type, new object[] { this }));

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

            extensions.Sort((a, b) =>
            {
                return a.GetOrder().CompareTo(b.GetOrder());
            });
        }

        public bool TryGet<TExtension2>(out TExtension2 result) where TExtension2 : TExtension
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

        public sealed override void RunExtensions()
        {
            foreach (var item in extensions)
            {
                item.Run();
            }
        }

        public sealed override void InitExtensions()
        {
            foreach (var item in extensions)
            {
                item.Init();
            }
        }

        public sealed override void Cleanup()
        {
            foreach (var item in extensions)
            {
                item.Cleanup();
            }
        }

        protected List<Extension> GetExtensions() => extensions;
    }
}