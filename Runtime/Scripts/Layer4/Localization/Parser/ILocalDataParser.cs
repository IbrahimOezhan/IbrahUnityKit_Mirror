#region

using System.Collections.Generic;

#endregion

namespace IbrahKit.Localization
{
    [System.Serializable]
    public abstract class ILocalDataParser
    {
        public abstract void Parse(string data, Dictionary<string, string[]> dictionary, int languageIndex, int languageCount);
    }
}