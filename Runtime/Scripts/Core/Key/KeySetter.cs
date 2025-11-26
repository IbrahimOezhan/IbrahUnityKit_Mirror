using IbrahKit;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class KeySetter
{
    [SerializeField, ValueDropdown(nameof(GetKeys))] private string db;

    public IEnumerable<string> GetKeys()
    {
        Key_Database_Finder.TryGetDatabase(out Key_Database db);

        return db.GetKeys();
    }
}
