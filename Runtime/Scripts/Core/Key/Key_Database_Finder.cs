#if UNITY_EDITOR

using IbrahKit.Debugging;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace IbrahKit
{
    public static class Key_Database_Finder
    {
        static Key_Database cached;

        public static List<string> GetKeys(string name)
        {
            if (!TryGetDatabase(out Key_Database db))
            {
                return new() { "NO_DB" };
            }

            if (!db.GetTables().TryGetValue(name, out List<string> keys))
            {
                return new() { "NO_TABLE" };
            }

            return keys;
        }

        public static bool TrySetKeys(string name, IEnumerable<string> keys)
        {
            if (!TryGetDatabase(out Key_Database db))
            {
                return false;
            }

            if (db.GetTables().ContainsKey(name))
            {
                db.GetTables()[name] = keys.ToList();
            }
            else
            {
                db.GetTables().Add(name, keys.ToList());
            }

            EditorUtility.SetDirty(db);

            return true;
        }

        public static bool TrySetKeys<TKey>(string name, IEnumerable<TKey> keys) where TKey : IKey
        {
            return TrySetKeys(name, keys.Select(x => x.GetKey()));
        }

        public static bool TryGetDatabase(out Key_Database db)
        {
            if (cached != null)
            {
                db = cached;
                return true;
            }

            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Key_Database)}");

            switch (guids.Length)
            {
                case 1:
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    cached = AssetDatabase.LoadAssetAtPath<Key_Database>(path);
                    db = cached;
                    return true;
                case 0:
                    IbrahDebug.LogWarning("No DB found");
                    break;
                default:
                    IbrahDebug.LogWarning("More than 1 DB found");
                    break;
            }

            db = null;
            return false;
        }
    }
}

#endif