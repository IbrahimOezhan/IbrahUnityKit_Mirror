#region

using System.Collections.Generic;

#endregion

namespace IbrahKit.Save
{
    public interface ISaveChooser
    {
        public Save_Object Choose(List<Save_Object> saves);
    }
}