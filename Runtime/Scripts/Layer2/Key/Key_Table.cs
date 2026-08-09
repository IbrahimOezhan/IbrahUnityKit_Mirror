#region

using System;
using System.Collections.Generic;
using IbrahKit.Keys;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;

#endregion

public class Key_Table<TKey, TTable> : SerializedScriptableObjectSingleton<TTable> where TTable : Key_Table<TKey, TTable>
    where TKey : Key_Reference<TKey, TTable>, new()
{
    [OdinSerialize, ReadOnly, InlineProperty]
    private List<string> values = new();

    public List<string> Values
    {
        get => values;
        set => values = value;
    }
}