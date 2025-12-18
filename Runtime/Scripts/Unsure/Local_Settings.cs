#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace IbrahKit.Localization
{
    public class Local_Settings : EditorWindow
    {
        private const string PrefKey = "local_config_asset_path";
        private ObjectField objectField;
        private Local_Manager_Data configAsset;

        [MenuItem("IbrahKit/Local Settings")]
        public static void ShowWindow()
        {
            var wnd = GetWindow<Local_Settings>();
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

            string path = EditorPrefs.GetString(PrefKey, "");
            if (!string.IsNullOrEmpty(path))
                configAsset = AssetDatabase.LoadAssetAtPath<Local_Manager_Data>(path);

            objectField.value = configAsset;

            objectField.RegisterValueChangedCallback(evt =>
            {
                configAsset = evt.newValue as Local_Manager_Data;
                string newPath = AssetDatabase.GetAssetPath(configAsset);
                EditorPrefs.SetString(PrefKey, newPath);
            });

            rootVisualElement.Add(objectField);
        }

        public static Local_Manager_Data Config()
        {
            string path = EditorPrefs.GetString(PrefKey, "");

            if (string.IsNullOrEmpty(path)) return null;

            return AssetDatabase.LoadAssetAtPath<Local_Manager_Data>(path);
        }
    }

}
#endif