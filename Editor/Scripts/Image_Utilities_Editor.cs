#region

using System.IO;
using System.Linq;
using IbrahKit.Debugging;
using IbrahKit.Utilities;
using UnityEditor;
using UnityEngine;

#endregion

namespace IbrahKit.Editor
{
    public static class Image_Utilities_Editor
    {
        [MenuItem("Assets/IbrahKit/Grayscale Sprite", true)]
        private static bool ValidateLogSelectedTransformName()
        {
            Object[] objects = Selection.objects;

            return objects.All(t => t is Texture2D);
        }

        [MenuItem("Assets/IbrahKit/Grayscale Sprite", priority = 0)]
        public static void Grayscale()
        {
            string[] filePaths = Selection.assetGUIDs;
            
            if (filePaths.Length == 0)
            {
                return;
            }

            foreach (var t in filePaths)
            {
                string path = AssetDatabase.GUIDToAssetPath(t);

                TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);

                importer.textureType = TextureImporterType.Default;

                importer.alphaSource = TextureImporterAlphaSource.FromInput;

                importer.isReadable = true;

                importer.textureCompression = TextureImporterCompression.Uncompressed;

                importer.SaveAndReimport();

                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

                IbrahDebug.Log($"Format: {tex.format}, IsReadable: {tex.isReadable}, Name: {tex.name}");

                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);

                Sprite grayscaleSprite = sprite.texture.Grayscale().ToSprite(sprite.pixelsPerUnit);

                byte[] bytes = grayscaleSprite.ToByteArray();

                FileInfo fileInfo = new(path);

                string dirName = fileInfo.DirectoryName;

                string fileName = fileInfo.Name;

                string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);

                string newPath = Path.Combine(dirName, fileNameNoExtension + "_grayscale" + ".png");

                while (File.Exists(newPath))
                {
                    newPath = Path.Combine(dirName,
                        fileNameNoExtension + "_grayscale_" + Random.Range(0, 9999) + ".png");
                }

                File.WriteAllBytes(newPath, bytes);

                IbrahDebug.Log("Test");

                importer.textureType = TextureImporterType.Sprite;

                importer.SaveAndReimport();
            }

            AssetDatabase.Refresh();
        }
    }
}