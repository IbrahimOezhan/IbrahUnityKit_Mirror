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
    public abstract class Save_Manager<T> : MonoBehaviourSingletonDontDestroyOnLoad<T> where T : MonoBehaviourSingletonDontDestroyOnLoad<T>
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

        public List<Save_File> GetSaveFiles()
        {
            List<Save_File> saves = new List<Save_File>();

            foreach (string f in GetRawSaveFiles())
            {
                string file = f;

                try
                {
                    pipelines.ForEach(x => { file = x.OnDeserialize(f); });

                    Save_File saveFile = Json_Utilities.Deserialize<Save_File>(file);

                    saves.Add(saveFile);
                }
                catch
                {
                }
            }

            return saves;
        }

        public List<Save_Object> GetSaveObjects(List<Save_File> files)
        {
            return files.Select(x => x.TryLoad()).ToList();
        }

        public Save_Object GetNew()
        {
            return new Save_File(parser).TryLoad();
        }

        public Save_Object GetBest(List<Save_Object> saves)
        {
            return chooser.Choose(saves);
        }

        public void SaveToFile(Save_File saveFile, string fileName)
        {
            string json = Json_Utilities.Serialize(saveFile);

            pipelines.ForEach(x => { json = x.OnSerialize(json); });

            File.WriteAllText(fileName, json);
        }
    }
}