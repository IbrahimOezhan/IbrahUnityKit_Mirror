using IbrahKit.Input;
using UnityEditor;
using UnityEditor.UI;

namespace IbrahKit.Editor
{
    [CustomEditor(typeof(UI_CursorHandler))]
    public class UI_CursorHandlerEditor : GraphicEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_RaycastTarget"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}