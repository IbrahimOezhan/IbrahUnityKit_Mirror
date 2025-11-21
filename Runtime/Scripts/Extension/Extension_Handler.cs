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
            return Type_Utilities.GetAllTypesDropdownFormat(typeof(TExtension), extensions.Select(x => x.GetType()));
        }

        public void AddExtension()
        {
            SortList();

            Type[] types = Type_Utilities.GetAllTypes(typeof(TExtension));

            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].FullName == extension)
                {
                    extensions.Add((TExtension)Activator.CreateInstance(types[i], new object[] { this }));

                    SortList();

                    break;
                }
            }

            extension = NONE;
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

        public override void RunExtensions()
        {
            foreach (var item in extensions)
            {
                item.Run();
            }
        }

        public override void InitExtensions()
        {
            foreach (var item in extensions)
            {
                item.Init();
            }
        }

        public override void Cleanup()
        {
            foreach (var item in extensions)
            {
                item.Cleanup();
            }
        }
    }
}