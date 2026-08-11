#region

using System;
using IbrahKit.Keys;

#endregion

namespace IbrahKit.Localization
{
    /// <summary>
    ///     Subclass for Localization Keys
    /// </summary>
    [Serializable]
    public class Local_Key : Key_Reference<Local_Key, Local_Key_Table>
    {
        public static implicit operator Local_Key(string value)
        {
            return new Local_Key { key = value };
        }

        private class Processor : Key_Processor
        {
        }
    }
}