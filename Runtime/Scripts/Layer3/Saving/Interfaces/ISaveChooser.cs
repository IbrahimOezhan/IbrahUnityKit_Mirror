using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit.Save
{
    public interface ISaveChooser
    {
        public SaveObject Choose(List<SaveObject> saves);
    }
}
