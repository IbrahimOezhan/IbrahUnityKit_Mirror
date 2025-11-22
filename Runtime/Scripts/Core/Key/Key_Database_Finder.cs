#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IbrahKit
{
    public static class Key_Database_Finder
    {
        static Key_Database cached;

        public static List<string> GetKeys(string name)
        {
            if (GetDatabase(out Key_Database db))
            {
                if (db.Get().TryGetValue(name, out List<string> keys))
                {
                    return keys;
                }
            }

            return new() { "Test" };
        }

        public static bool TrySetKeys(string name, List<string> keys)
        {
            if (GetDatabase(out Key_Database db))
            {
                if (db.Get().ContainsKey(name))
                {
                    db.Get()[name] = keys;
                }
                else
                {
                    db.Get().Add(name, keys);
                }

                EditorUtility.SetDirty(db);
                AssetDatabase.SaveAssets();

                return true;
            }

            return false;
        }

        public static bool GetDatabase(out Key_Database db)
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
                    Debug.LogWarning("No DB found");
                    break;
                default:
                    Debug.LogWarning("More than 1 DB found");
                    break;
            }

            db = null;
            return false;
        }
    }
}

#endif