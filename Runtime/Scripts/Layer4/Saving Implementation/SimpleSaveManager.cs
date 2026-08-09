#region

using System;
using IbrahKit.Debugging;
using Sirenix.Serialization;
using UnityEngine;

#endregion

namespace IbrahKit.Save.Simple
{
    public class SimpleSaveManager : Save_Manager
    {
        [OdinSerialize, SerializeField] private Save_Object saveObject;
        private Save_Dictionary dict;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            saveObject = GetBest(GetSaveObjects(GetSaveFiles()));

            if (saveObject == null)
            {
                IbrahDebug.LogWarning("No save found. Creating new save");
                saveObject = GetNew();
            }
            else
            {
                IbrahDebug.LogWarning("Successfully loaded save");
            }

            dict = saveObject.Get<Save_Dictionary>();
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            ToSaveFile(saveObject, "save.json");
        }

        public override Save_Object GetLoadedSave()
        {
            return saveObject;
        }

        protected override (ISaveVersionParser, ISaveChooser, ISavePipeline[]) Init()
        {
            return (new SimpleSaveVersionParser(), new SimpleSaveChooser(), Array.Empty<ISavePipeline>());
        }

        public Save_Dictionary GetDict() => dict;
    }
}