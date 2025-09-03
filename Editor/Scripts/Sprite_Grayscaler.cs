using IbrahKit;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = IbrahKit.Debug;

public class Sprite_Grayscaler
{
    [MenuItem("Assets/IbrahKit/Grayscale Sprite", priority = 0)]
    public static void Grayscale()
    {
        string[] filePaths = Selection.assetGUIDs;
        Object[] objects = Selection.objects;

        if (filePaths.Length == 0)
        {
            return;
        }

        for (int i = 0; i < filePaths.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(filePaths[i]);

            TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);

            importer.textureType = TextureImporterType.Default;

            importer.alphaSource = TextureImporterAlphaSource.FromInput;

            importer.isReadable = true;

            importer.textureCompression = TextureImporterCompression.Uncompressed;

            importer.SaveAndReimport();

            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

            Debug.Log($"Format: {tex.format}, IsReadable: {tex.isReadable}, Name: {tex.name}");

            Sprite s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);

            Sprite grayscale = Image_Utitlities.GrayscaleSprite(s);

            byte[] bytes = Image_Utitlities.ImageToByteArray(grayscale);

            FileInfo fileInfo = new(path);

            string dirName = fileInfo.DirectoryName;

            string fileName = fileInfo.Name;

            string fileNameNoExtension = Path.GetFileNameWithoutExtension(fileName);

            string newPath = Path.Combine(dirName, fileNameNoExtension + "_grayscale" + ".png");

            while (File.Exists(newPath))
            {
                newPath = Path.Combine(dirName, fileNameNoExtension + "_grayscale_" + Random.Range(0, 9999) + ".png");
            }

            File.WriteAllBytes(newPath, bytes);

            Debug.Log("Test");

            importer.textureType = TextureImporterType.Sprite;

            importer.SaveAndReimport();
        }

        AssetDatabase.Refresh();
    }
}
