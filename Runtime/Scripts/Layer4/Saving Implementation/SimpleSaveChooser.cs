#region

using System.Collections.Generic;
using System.Linq;
using IbrahKit.Save;

#endregion

public class SimpleSaveChooser : ISaveChooser
{
    public SaveObject Choose(List<SaveObject> saves)
    {
        saves.Sort((a, b) => a.CompareTo(b));
        return saves.First();
    }
}