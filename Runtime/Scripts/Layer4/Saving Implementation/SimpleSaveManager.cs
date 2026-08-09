#region

using System;
using IbrahKit.Save;

#endregion

namespace IbrahKit.Save.Simple
{
    public class SimpleSaveManager : Save_Manager
    {
        private Save_Dictionary dict;

        private Save_Object saveObject;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            saveObject = GetBest(GetSaveObjects(GetSaveFiles()));

            dict = saveObject.Get(new Save_Dictionary());
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