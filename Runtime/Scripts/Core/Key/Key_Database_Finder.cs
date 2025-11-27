#if UNITY_EDITOR

using IbrahKit.Debugging;
using System.Collections.Generic;
using UnityEditor;

namespace IbrahKit
{
    public static class Key_Database_Finder
    {
        static Key_Database cached;

        public static List<string> GetKeys(string name)
        {
            if (TryGetDatabase(out Key_Database db))
            {
                if (db.GetPairs().TryGetValue(name, out List<string> keys))
                {
                    return keys;
                }
            }

            return new() { "Test" };
        }

        public static bool TrySetKeys(string name, List<string> keys)
        {
            if (TryGetDatabase(out Key_Database db))
            {
                if (db.GetPairs().ContainsKey(name))
                {
                    db.GetPairs()[name] = keys;
                }
                else
                {
                    db.GetPairs().Add(name, keys);
                }

                EditorUtility.SetDirty(db);
                AssetDatabase.SaveAssets();

                return true;
            }

            return false;
        }

        public static bool TryGetDatabase(out Key_Database db)
        {
            if (cached != null)
            {
                db = cached;
                return true;
            }

            string[] guids = AssetDatabase.FindAssets("t:KeyDatabase");

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