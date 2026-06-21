#region

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

#endregion

namespace IbrahKit.Utilities
{
    /// <summary>
    /// Contains asset related utilities
    /// </summary>
    public static class Asset_Utilities
    {
#if UNITY_EDITOR
        public static TScriptableObject CreateScriptableObject<TScriptableObject>(string path) where TScriptableObject : ScriptableObject
        {
            if (path.IsEmpty()) throw new ArgumentException("Path is empty");
            
            TScriptableObject asset = ScriptableObject.CreateInstance<TScriptableObject>();

            string directory = Path.GetDirectoryName(path);

            if (directory == null) throw new NullReferenceException("Directory name is null");
            
            if (!Directory.Exists(directory))Directory.CreateDirectory(directory);

            AssetDatabase.CreateAsset(asset, path);

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();

            return asset;
        }
#endif
    }
}