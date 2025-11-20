using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace IbrahKit
{
    public static class FileSystem_Utilities
    {
        private const string myGames = "My Games";

#if UNITY_EDITOR
        [MenuItem("IbrahKit/OpenPath")]
#endif
        public static void OpenPath()
        {
            Process.Start(GetGamePath());
        }

        public static string GetGamePath()
        {
            string gamePath;

            if (!Application.isMobilePlatform)
            {
                gamePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), myGames);
            }
            else
            {
                gamePath = Application.persistentDataPath;
            }

            gamePath = Path.Combine(gamePath, Application.productName);

            if (!Directory.Exists(gamePath)) Directory.CreateDirectory(gamePath);

            return gamePath;
        }

        public static void WriteToFile(string filePath, string fileContent, bool ifDoesntExist = false)
        {
            if (File.Exists(filePath) && ifDoesntExist) return;

            using StreamWriter writer = new(filePath);
            writer.Write(fileContent);
        }

        public static string ReadFromFile(string filePath)
        {
            string fileContent = string.Empty;

            bool fileExists = File.Exists(filePath);

            if (fileExists)
            {
                using StreamReader reader = new(filePath);
                fileContent = reader.ReadToEnd();
            }
            else
            {
                IbrahDebug.Log("File at " + filePath + " does not exist");
            }

            return fileContent;
        }
    }
}


