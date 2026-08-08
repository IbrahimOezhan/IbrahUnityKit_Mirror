#region

using System;
using System.Collections.Generic;
using IbrahKit.Keys;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;

#endregion

public class Key_Table<TKey, TTable> : SerializedScriptableObject where TTable : Key_Table<TKey, TTable>
    where TKey : Key_Reference<TKey, TTable>, new()
{
    [OdinSerialize, ReadOnly, InlineProperty]
    private List<string> values = new();

    public List<string> Values
    {
        get => values;
        set => values = value;
    }

#if UNITY_EDITOR

    public static TTable Instance
    {
        get
        {
            Type type = typeof(TTable);

            string[] guids = AssetDatabase.FindAssets($"t:{type.Name}");

            switch (guids.Length)
            {
                case 1:
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    return AssetDatabase.LoadAssetAtPath<TTable>(path);
                case 0:
                    throw new Exception($"No Table of type {typeof(TTable)} found");
                default:
                    throw new Exception($"More than 1 Table of type {typeof(TTable)} found");
            }
        }
    }

#endif
}