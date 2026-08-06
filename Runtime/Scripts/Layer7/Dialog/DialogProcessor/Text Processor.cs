#region

using System;

#endregion

namespace IbrahKit.Dialog
{
    [Serializable,DialogTag("")]
    public abstract class DialogProcessor
    {
        public abstract string Process(string text);
    }
}