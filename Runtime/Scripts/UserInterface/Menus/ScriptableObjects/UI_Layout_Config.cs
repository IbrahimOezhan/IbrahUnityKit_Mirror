using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "NewUILayoutConfig", menuName = "IbrahKit/UILayoutConfig")]
    public class UI_Layout_Config : ScriptableObject
    {
        private static UI_Layout_Config active;

        [SerializeField, OnValueChanged(nameof(OnValueChanged))] private List<string> layouts = new();

        private void OnValueChanged()
        {
            if (active == null) active = this;

            if (active == this)
            {
                List<string> list = new(layouts)
            {
                "None"
            };

                Dropdown_Utilities.CreateDropdown(list, UI_Menu_Manager.UILAYOUTKEY);
            }
        }

        [Button(), ShowIf("@active != this")]
        private void SetActive()
        {
            active = this;
        }
    }
}