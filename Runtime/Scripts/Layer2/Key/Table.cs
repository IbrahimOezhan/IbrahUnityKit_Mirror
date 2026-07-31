#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Debugging;
using IbrahKit.Keys;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;

#endregion

public class Table<TKey, TTable> : SerializedScriptableObject where TTable : Table<TKey, TTable>
    where TKey : Key_Reference<TKey, TTable>, new()
{
    [OdinSerialize, ReadOnly, InlineProperty]
    private List<string> values = new();

    public List<string> Values
    {
        get => values;
        set => values = value;
    }

    public static TTable Instance
    {
        get
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(TTable)}");

            switch (guids.Length)
            {
                case 1:
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    return AssetDatabase.LoadAssetAtPath<TTable>(path);
                case 0:
                    IbrahDebug.LogWarning("No DB found");
                    throw new Exception("No DB found");
                default:
                    IbrahDebug.LogWarning("More than 1 DB found");
                    throw new Exception("More than 1 DB found");
            }
        }
    }
}