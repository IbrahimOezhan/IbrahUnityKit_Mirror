#region

#endregion

namespace IbrahKit.Editor
{
# if UNITY_6000_5_OR_NEWER

# else // Not required in 6.5 or Newer. Use the native Raycast_Receiver instead 
    [CustomEditor(typeof(UI_Raycast_Receiver))]
    public class UI_CursorHandlerEditor : GraphicEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_RaycastTarget"));

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}