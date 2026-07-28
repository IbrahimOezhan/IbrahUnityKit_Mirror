#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IbrahKit.Debugging;
using IbrahKit.Utilities;

#endregion

namespace IbrahKit.Save
{
    internal partial class Save
    {
        private readonly Save_State[] fileState = new Save_State[0];

        private readonly HashSet<Savable> inUse = new();

        private readonly Dictionary<string, Savable> loadable = new();

        private readonly Save_State state = Save_State.Valid;

        private string folderPath;
        private string key;

        private string version;

        public Save(List<string> names, List<Savable> savables, string folderPath, string key, bool encrypt)
        {
            Init(folderPath, key);

            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }

            Directory.CreateDirectory(folderPath);

            for (int i = 0; i < savables.Count; i++)
            {
                Return(names[i], savables[i], encrypt);

                loadable.Add(names[i], savables[i]);
            }
        }

        public Save(string folderPath, string key)
        {
            string[] filePaths;

            Init(folderPath, key);

            try
            {
                filePaths = Directory.GetFiles(folderPath);
            }
            catch (Exception ex)
            {
                IbrahDebug.LogWarning(ex.Message);

                state = Save_State.Corrupted;

                return;
            }

            int LENGTH = filePaths.Length;

            fileState = new Save_State[LENGTH];

            var encryptionSuccess = new bool[LENGTH];

            for (int i = 0; i < LENGTH; i++)
            {
                if (!FileSystem_Utilities.TryReadFromFile(filePaths[i], out string fileContents))
                {
                    IbrahDebug.LogWarning($"{filePaths[i]} doesnt exist");

                    fileState[i] = Save_State.Corrupted;

                    continue;
                }

                encryptionSuccess[i] = Save_Utilities.Decrypt(fileContents, key, out fileContents);

                if (!encryptionSuccess[i])
                {
                    fileState[i] = Save_State.Corrupted;

                    continue;
                }

                ValidateTask validationTask = new(filePaths[i], fileContents, fileState[i] == Save_State.Corrupted);

                fileState[i] = validationTask.GetFileState();

                if (fileState[i] != Save_State.Corrupted)
                    loadable.Add(Path.GetFileName(filePaths[i]), validationTask.GetSavable());

                state = (Save_State)MathF.Max((int)state, (int)fileState[i]);
            }
        }

        public void Init(string folderPath, string key)
        {
            this.folderPath = folderPath;

            this.key = key;

            version = Path.GetFileName(folderPath);
        }

        /// <summary>
        ///     Gets the amount of valid files in this save
        /// </summary>
        /// <returns>The amount of valid files</returns>
        public int GetValidFileCount()
        {
            int amount = 0;

            for (int i = 0; i < fileState.Length; i++)
            {
                if (fileState[i] == Save_State.Valid) amount++;
            }

            return amount;
        }

        /// <summary>
        ///     Gets the total state of the this save
        /// </summary>
        /// <returns>Gets the total state of this save</returns>
        internal Save_State GetState() => state;

        /// <summary>
        ///     Gets the keys of this save
        /// </summary>
        /// <returns>The keys of this save</returns>
        public List<string> GetKeys() => loadable.Keys.ToList();

        /// <summary>
        ///     Gets the savables of this save
        /// </summary>
        /// <returns>Gets the savables</returns>
        public List<Savable> GetSavables() => loadable.Values.ToList();

        /// <summary>
        ///     Deletes this save
        /// </summary>
        public void Delete()
        {
            Directory.Delete(folderPath, true);
        }

        /// <summary>
        ///     Loads a savable from this save
        /// </summary>
        /// <param name="name">The name of the savable to load</param>
        /// <param name="defaultValue">The default value of the savable to load incase the savable doesnt exist</param>
        /// <returns>The loaded saveable</returns>
        /// <exception cref="SaveInUseException">Throws if the saveable has already been loaded by another object</exception>
        public Savable Load(string name, Savable defaultValue)
        {
            if (!loadable.TryGetValue(name, out Savable savable)) // Does not exist so return default
            {
                loadable.Add(name, defaultValue);

                inUse.Add(defaultValue);

                return defaultValue;
            }

            if (inUse.Contains(savable))
            {
                throw new SaveInUseException();
            }

            inUse.Add(savable);

            return savable;
        }

        /// <summary>
        ///     Returns a savable
        /// </summary>
        /// <param name="name">The name of the savable to return</param>
        /// <param name="value">The value of the saveable to return</param>
        /// <param name="encrypt">Whether to encrypt the returned savable</param>
        /// <param name="stillInUse">
        ///     Whether its still in use. In that case don't remove it from the inUse list. This can be used
        ///     for saving the game without quitting
        /// </param>
        public void Return(string name, Savable value, bool encrypt, bool stillInUse = false)
        {
            try
            {
                Type t = Save_Utilities.GetSavableType(value);

                string json = Json_Utilities.Serialize(value, t);

                string fileContent = encrypt ? String_Utilities.Encrypt(json, key) : json;

                using StreamWriter streamWriter = new(Path.Combine(folderPath, name));

                streamWriter.Write(fileContent);

                if (!stillInUse) inUse.Remove(value);
            }
            catch (Exception ex)
            {
                IbrahDebug.LogWarning($"{name} - {ex.Message}");
            }
        }

        /// <summary>
        ///     Returns all loaded savables
        /// </summary>
        /// <param name="encrypt">Whether to encrypt the savables</param>
        /// <param name="stillInuse">
        ///     Whether they are still in use. In that case don't remove them from the inUse list. This can be
        ///     used for saving the game without quitting
        /// </param>
        public void FlushAll(bool encrypt, bool stillInuse = true)
        {
            foreach (var item in loadable)
            {
                if (inUse.Contains(item.Value))
                {
                    Return(item.Key, item.Value, encrypt, stillInuse);
                }
            }
        }

        /// <summary>
        ///     Compare this save's version to another save
        /// </summary>
        /// <param name="otherSave">The second save to compare to</param>
        /// <returns>An integer representing what save has the lower version</returns>
        public int CompareTo(Save otherSave) => String_Utilities.CompareVersions(version, otherSave.version);
    }
}