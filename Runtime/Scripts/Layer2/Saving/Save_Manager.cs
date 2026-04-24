#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using IbrahKit.Core;
using IbrahKit.Debugging;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Save
{
    /// <summary>
    /// A script that manages loading data on game start and saving it when you close the game
    /// </summary>
    [DefaultExecutionOrder(Execution_Order.save)]
    public partial class Save_Manager : Manager_Global<Save_Manager>
    {
        private const string GENERIC_KEY = "Generic";

        private const string KEY = "a3c9e7r3gf3d5e7";

        [SerializeField] private bool encrypt;

        private static Save currentSave;

        private static Save_Dictionary generic;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            string saveFolderPath = Path.Combine(FileSystem_Utilities.GetGamePath(), "Saves");

            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }

            currentSave ??= GetCurrentFolder(saveFolderPath, KEY);

            generic ??= (Save_Dictionary)currentSave.Load(GENERIC_KEY, new Save_Dictionary());
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            currentSave.FlushAll(encrypt);
        }

        public bool TryLoad<T>(string name, out T result, bool logWarning = true) where T : Savable, new()
        {
            Savable savable = currentSave.Load(name, new T());

            if (savable is T casted)
            {
                result = casted;
                return true;
            }

            if (logWarning)
            {
                IbrahDebug.LogWarning($"Savable of the name {name} is not of type {typeof(T)}");
            }

            result = default;
            return false;
        }

        public void Return(string name, Savable value, bool stillInUse = false)
        {
            currentSave.Return(name, value, encrypt, stillInUse);
        }

        public Save_Dictionary GetGeneric()
        {
            return generic;
        }

        private Save GetCurrentFolder(string saveFolderPath, string key)
        {
            string thisVersionPath = Path.Combine(saveFolderPath, Application.version);

            Save bestSave = GetBestFolder(thisVersionPath, saveFolderPath, key);

            if (bestSave.GetState() == Save_State.Valid) return bestSave;

            return new(bestSave.GetKeys(), bestSave.GetSavables(), thisVersionPath, key, encrypt);
        }

        private Save GetBestFolder(string thisVersionFolder, string saveFolderPath, string key)
        {
            List<string> folders = Directory.GetDirectories(saveFolderPath)
                .Where(x => String_Utilities.TryParseVersion(Path.GetFileName(x))).ToList();

            if (folders.Count == 0)
            {
                IbrahDebug.Log("Returned new save folder");

                return new(new(), new(), Path.Combine(saveFolderPath, Application.version), key, encrypt);
            }

            List<Save> saves = new();

            for (int i = 0; i < folders.Count; i++)
            {
                Save save = new(folders[i], key);

                if (Path.GetFileName(folders[i]) == Path.GetFileName(thisVersionFolder) &&
                    save.GetState() == Save_State.Valid)
                {
                    IbrahDebug.Log("Returned save folder with same version");

                    return save;
                }

                saves.Add(save);
            }

            saves = saves.OrderByDescending(x => x.GetValidFileCount()).ThenBy(x => x.GetState()).ToList();

            IbrahDebug.Log("Returned best save folder");

            return saves[0];
        }
    }
}