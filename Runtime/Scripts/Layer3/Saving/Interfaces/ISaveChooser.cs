#region

using System.Collections.Generic;

#endregion

namespace IbrahKit.Save
{
    public interface ISaveChooser
    {
        public SaveObject Choose(List<SaveObject> saves);
    }
}