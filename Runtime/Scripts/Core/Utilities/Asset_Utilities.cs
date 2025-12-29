using System.IO;
using UnityEditor;
using UnityEngine;

namespace IbrahKit
{
    public static class Asset_Utilities
    {
#if UNITY_EDITOR
        public static T CreateScriptableObject<T>(string path) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            string directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            AssetDatabase.CreateAsset(asset, path);

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();

            return asset;
        }
#endif
    }
}