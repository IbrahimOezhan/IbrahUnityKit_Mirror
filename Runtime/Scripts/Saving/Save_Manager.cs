using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace IbrahKit.Save
{
    /// <summary>
    /// A script that manages loading data on game start and saving it when you close the game
    /// </summary>
    [DefaultExecutionOrder(Execution_Order.save)]
    public partial class Save_Manager : Manager_DDOL<Save_Manager>
    {
        private const string GENERIC_KEY = "Generic";

        private const string KEY = "a3c9e7r3gf3d5e7";

        [SerializeField] private bool encrypt;

        private static Save currentSave;

        private static GenericSaveData generic;

        protected override void OnAwake()
        {
            base.OnAwake();

            string saveFolderPath = Path.Combine(FileSystem_Utilities.GetGamePath(), "Saves");

            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }

            currentSave ??= GetCurrentFolder(saveFolderPath, KEY);

            generic ??= (GenericSaveData)currentSave.Load(GENERIC_KEY, new GenericSaveData());

        }

        private void OnDestroy()
        {
            if (GetInstance() == this)
            {
                currentSave.FlushAll(encrypt);
            }
        }

        public Savable Load(string name, Savable defaultValue)
        {
            Savable savable = currentSave.Load(name, defaultValue);

            return savable;
        }

        public bool TryLoad<T>(string name, Savable defaultValue, out T result, bool logWarning = true) where T : Savable
        {
            Savable savable = currentSave.Load(name, defaultValue);

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

        public GenericSaveData GetGeneric()
        {
            return generic;
        }

        private Save GetCurrentFolder(string saveFolderPath, string key)
        {
            string thisVersionPath = Path.Combine(saveFolderPath, Application.version);

            Save bestSave = GetBestFolder(thisVersionPath, saveFolderPath, key);

            if (bestSave.GetState() == Save.State.Valid) return bestSave;

            return new(bestSave.GetKeys(), bestSave.GetSavables(), thisVersionPath, key, encrypt);
        }

        private Save GetBestFolder(string thisVersionFolder, string saveFolderPath, string key)
        {
            List<string> folders = Directory.GetDirectories(saveFolderPath).ToList();

            folders.RemoveAll(x => !String_Utilities.TryParseVersion(Path.GetFileName(x)));

            if (folders.Count == 0)
            {
                IbrahDebug.Log("Returned new save folder");

                return new(new(), new(), Path.Combine(saveFolderPath, Application.version), key, encrypt);
            }

            List<Save> saves = new();

            for (int i = 0; i < folders.Count; i++)
            {
                Save save = new(folders[i], key);

                saves.Add(save);

                if (Path.GetFileName(folders[i]) == Path.GetFileName(thisVersionFolder) && save.GetState() == Save.State.Valid)
                {
                    IbrahDebug.Log("Returned save folder with same version");

                    return save;
                }
            }

            saves = saves.OrderByDescending(x => x.GetValidFileCount()).ThenBy(x => x.GetState()).ToList();

            IbrahDebug.Log("Returned best save folder");

            return saves[0];
        }
    }
}