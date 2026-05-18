#if UNITY_EDITOR

#region

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#endregion

namespace IbrahKit.Localization
{
    /// <summary>
    /// Adds a field that allows dragging in a Local_Managaer_Data that can then be accessed using the GetConfig() Method.
    /// This can be used to retrieve localizations on objects during edit-time while not in the same scene as the local manager
    /// </summary>
    public class Local_Editor_Settings : EditorWindow
    {
        private const string PREF_KEY = "local_config_asset_path";
        
        private ObjectField objectField;
        
        private Local_Manager_Data configAsset;

        [MenuItem("IbrahKit/Local Settings")]
        public static void ShowWindow()
        {
            var wnd = GetWindow<Local_Editor_Settings>();
            
            wnd.titleContent = new GUIContent("Local Settings");
            
            wnd.minSize = new Vector2(300, 80);
        }

        private void CreateGUI()
        {
            objectField = new ObjectField("Local Config")
            {
                objectType = typeof(Local_Manager_Data),
                allowSceneObjects = false
            };

            string path = EditorPrefs.GetString(PREF_KEY, string.Empty);
            
            if (!string.IsNullOrEmpty(path)) configAsset = AssetDatabase.LoadAssetAtPath<Local_Manager_Data>(path);

            objectField.value = configAsset;

            objectField.RegisterValueChangedCallback(evt =>
            {
                configAsset = evt.newValue as Local_Manager_Data;
                
                string newPath = AssetDatabase.GetAssetPath(configAsset);
                
                EditorPrefs.SetString(PREF_KEY, newPath);
            });

            rootVisualElement.Add(objectField);
        }

        public static Local_Manager_Data Config()
        {
            string path = EditorPrefs.GetString(PREF_KEY, string.Empty);

            if (string.IsNullOrEmpty(path)) return null;

            return AssetDatabase.LoadAssetAtPath<Local_Manager_Data>(path);
        }
    }
}
#endif