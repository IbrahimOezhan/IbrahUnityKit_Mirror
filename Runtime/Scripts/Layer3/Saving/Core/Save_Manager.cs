#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using IbrahKit.Core;
using IbrahKit.Manager;
using IbrahKit.Utilities;
using Sirenix.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Save
{
    /// <summary>
    ///     A script that manages loading data on game start and saving it when you close the game
    /// </summary>
    [DefaultExecutionOrder(Execution_Order.save)]
    public abstract class Save_Manager<T> : Manager_Global<T> where T : Manager_Global<T>
    {
        private ISaveChooser chooser;

        private ISaveVersionParser parser;
        private ISavePipeline[] pipelines;
        private string saveFolderPath;

        protected abstract (ISaveVersionParser, ISaveChooser, ISavePipeline[]) Init();

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            (parser, chooser, pipelines) = Init();

            string saveFolderPath = Path.Combine(FileSystem_Utilities.GetGamePath(), "Saves");

            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }
        }

        public string[] GetRawSaveFiles()
        {
            string[] files = Directory.GetFiles(saveFolderPath);

            return files.Select(File.ReadAllText).ToArray();
        }

        public List<SaveFile> GetSaveFiles()
        {
            List<SaveFile> saves = new List<SaveFile>();

            foreach (string f in GetRawSaveFiles())
            {
                string file = f;

                try
                {
                    pipelines.ForEach(x => { file = x.OnDeserialize(f); });

                    SaveFile saveFile = Json_Utilities.Deserialize<SaveFile>(file);

                    saves.Add(saveFile);
                }
                catch
                {
                }
            }

            return saves;
        }

        public List<SaveObject> GetSaveObjects(List<SaveFile> files)
        {
            return files.Select(x => x.TryLoad()).ToList();
        }

        public SaveObject GetNew()
        {
            return new SaveFile(parser).TryLoad();
        }

        public SaveObject GetBest(List<SaveObject> saves)
        {
            return chooser.Choose(saves);
        }

        public void SaveToFile(SaveFile saveFile, string fileName)
        {
            string json = Json_Utilities.Serialize(saveFile);

            pipelines.ForEach(x => { json = x.OnSerialize(json); });

            File.WriteAllText(fileName, json);
        }
    }
}