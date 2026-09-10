#region

using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
#endif

#endregion

public class SerializedScriptableObjectSingleton<T> : SerializedScriptableObject
    where T : SerializedScriptableObjectSingleton<T>
{
    public static T Instance
    {
        get
        {
#if UNITY_EDITOR

            Type type = typeof(T);

            List<string> guids = AssetDatabase.FindAssets($"t:{type.Name}").ToList();

            string exclude = EditorPrefs.GetString("SOS_Exclude");

            exclude = Path.Combine(Application.dataPath, exclude);

            guids.RemoveAll(x =>
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return path.Contains(exclude);
            });
            
            switch (guids.Count)
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