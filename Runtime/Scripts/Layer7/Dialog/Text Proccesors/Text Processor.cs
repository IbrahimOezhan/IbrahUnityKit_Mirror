#region

using System;

#endregion

namespace IbrahKit.Dialog
{
    [Serializable]
    public abstract class TextProcessor
    {
        public abstract string Process(string text);
    }
}