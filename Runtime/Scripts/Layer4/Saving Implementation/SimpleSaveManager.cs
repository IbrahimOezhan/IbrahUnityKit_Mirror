#region

using System;
using IbrahKit.Save;

#endregion

public class SimpleSaveManager : Save_Manager<SimpleSaveManager>
{
    private Save_Dictionary dict;

    private Save_Object saveObject;

    protected override void InstanceAwake()
    {
        base.InstanceAwake();

        saveObject = GetBest(GetSaveObjects(GetSaveFiles()));

        dict = saveObject.Get(new Save_Dictionary());
    }

    protected override (ISaveVersionParser, ISaveChooser, ISavePipeline[]) Init()
    {
        return (new SimpleSaveVersionParser(), new SimpleSaveChooser(), Array.Empty<ISavePipeline>());
    }

    public Save_Object GetSave() => saveObject;

    public Save_Dictionary GetDict() => dict;
}