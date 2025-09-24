using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Interactive : UI_Base
    {
        private const string NONE = "None";

        [SerializeField, OnValueChanged(nameof(OnValueChanged)), ValueDropdown(nameof(GetAllSubtypes))]
        private string extension = NONE;

        [SerializeField, ReadOnly]
        private List<UI_Extension> extensions = new();

        public void OnValueChanged()
        {
            SortList();

            Type[] types = Type_Utilities.GetAllTypes(typeof(UI_Extension));

            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].Name == extension)
                {
                    UI_Extension extensionToAdd = gameObject.AddComponent(types[i]) as UI_Extension;

                    extensions.Add(extensionToAdd);

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

        [Button]
        private void UpdateExtensionList()
        {
            UI_Extension[] _extension = GetComponents<UI_Extension>();

            extensions = new(_extension.ToList());

            extensions.ForEach(x => x.ResetInit());

            SortList();
        }

        [Button]
        public void UpdateUI()
        {
            SortList();

            List<UI_Interactive> children = transform.BetterGetComponentsInChildren<UI_Interactive>();

            foreach (UI_Interactive child in children)
            {
                child.UpdateUI();
            }

            foreach (UI_Extension extension in extensions)
            {
                extension.Execute();
            }
        }

        private IEnumerable GetAllSubtypes()
        {
            return Type_Utilities.GetAllTypesDropdownFormat(typeof(UI_Extension));
        }

        public override void MenuUpdate()
        {
            UpdateUI();
        }

        public override void OnMenuEnabled()
        {
            UpdateUI();
        }
    }
}