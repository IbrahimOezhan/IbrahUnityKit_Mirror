#region

using System.Collections.Generic;

#endregion

public interface ILocalDataParser
{
    public void Parse(string data, Dictionary<string, string[]> dictionary, int languageIndex, int languageCount);
}