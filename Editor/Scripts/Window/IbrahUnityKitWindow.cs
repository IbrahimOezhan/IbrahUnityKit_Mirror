using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class IbrahUnityKitWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("IbrahKit/Setup")]
    public static void ShowExample()
    {
        IbrahUnityKitWindow wnd = GetWindow<IbrahUnityKitWindow>();
        wnd.titleContent = new GUIContent("IbrahUnityKitWindow");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
        
        TextField sos = root.Q<TextField>("SOS_Exclude");
        
        sos.value = EditorPrefs.GetString("SOS_Exclude");
        
        root.Q<Button>("Apply").clicked += () =>
        {
            EditorPrefs.SetString("SOS_Exclude", sos.value);
        };
    }
}
