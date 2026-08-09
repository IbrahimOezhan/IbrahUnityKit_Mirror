using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
{

    
    public static T Instance
    {
        get
        {
#if UNITY_EDITOR
            
            Type type = typeof(T);

            string[] guids = AssetDatabase.FindAssets($"t:{type.Name}");

            switch (guids.Length)
            {
                case 1:
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    return AssetDatabase.LoadAssetAtPath<T>(path);
                case 0:
                    throw new Exception($"No SO of type {typeof(T)} found");
                default:
                    throw new Exception($"More than 1 SO of type {typeof(T)} found");
            }
#else
                    throw new Exception($"This only works in the Editor");
#endif
            
        }
    }
    

}
