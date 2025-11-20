using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [Serializable]
    public class Extension_Handler<TExtension> : Extension_Handler_Base where TExtension : Extension
    {
        private const string NONE = "None";

        [SerializeField, OnValueChanged(nameof(OnValueChanged)), ValueDropdown(nameof(GetAllSubtypes))]
        private string extension = NONE;

        [SerializeField, ReadOnly]
        private List<TExtension> extensions = new();

        public void OnValueChanged()
        {
            SortList();

            Type[] types = Type_Utilities.GetAllTypes(typeof(TExtension));

            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].Name == extension)
                {
                    extensions.Add((TExtension)Activator.CreateInstance(types[i]));

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

        private IEnumerable GetAllSubtypes()
        {
            return Type_Utilities.GetAllTypesDropdownFormat(typeof(TExtension));
        }

        public override void RunExtensions()
        {
            foreach (var item in extensions)
            {
                item.Run();
            }
        }

        public void Cleanup()
        {
            foreach (var item in extensions)
            {
                item.Cleanup();
            }
        }
    }
}