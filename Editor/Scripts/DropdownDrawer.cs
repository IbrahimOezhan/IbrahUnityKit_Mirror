using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace IbrahKit
{
    [CustomPropertyDrawer(typeof(DropdownAttribute))]
    public class DropdownDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.targetObjects.Length > 1)
            {
                EditorGUI.LabelField(position, label.text, "Multi-editing is not supported");

                return;
            }

            DropdownAttribute dropdownAttribute = (DropdownAttribute)attribute;

            bool exists = Dropdown_Utilities.GetDropdown(dropdownAttribute.fileName, out IEnumerable<string> dropdown);

            if (!exists)
            {
                EditorGUI.LabelField(position, label.text, "File doesn't exist");
                return;
            }

            List<string> list =dropdown.ToList();

            if (list == null || list.Count() == 0)
            {
                EditorGUI.LabelField(position, label.text, "No options");
                return;
            }

            int selectedIndex = Mathf.Max(0, list.IndexOf(property.stringValue));

            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, list.ToArray());

            property.stringValue = list[selectedIndex];
        }
    }
}
