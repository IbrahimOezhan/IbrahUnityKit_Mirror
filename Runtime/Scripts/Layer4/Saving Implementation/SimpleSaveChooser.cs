using System.Collections.Generic;
using System.IO;
using System.Linq;
using IbrahKit.Debugging;
using IbrahKit.Save;
using UnityEngine;

public class SimpleSaveChooser : ISaveChooser
{
    public SaveObject Choose(List<SaveObject> saves)
    { 
        saves.Sort((a, b) => a.CompareTo(b));
        return saves.First();
    }
}
