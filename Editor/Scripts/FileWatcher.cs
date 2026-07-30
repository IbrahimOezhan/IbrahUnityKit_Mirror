#region

using System.Collections.Generic;
using IbrahKit.Core;
using UnityEditor;
using UnityEngine;

#endregion

namespace IbrahKit.Editor
{
    public class FileWatcher : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths, bool didDomainReload)
        {
            List<IFileWatcher> types = FindAllAssetsImplementing<IFileWatcher>();

            foreach (IFileWatcher item in types)
            {
                item.OnFileUpdaate();
            }
        }

        public static List<T> FindAllAssetsImplementing<T>() where T : class
        {
            var result = new List<T>();

            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                if (obj is T tObj) result.Add(tObj);
            }

            return result;
        }
    }
}