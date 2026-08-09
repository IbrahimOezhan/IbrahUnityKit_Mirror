#region

using System.Collections.Generic;
using System.Linq;
using IbrahKit.Save;

#endregion

public class SimpleSaveChooser : ISaveChooser
{
    public Save_Object Choose(List<Save_Object> saves)
    {
        saves.Sort((a, b) => a.CompareTo(b));
        return saves.FirstOrDefault();
    }
}