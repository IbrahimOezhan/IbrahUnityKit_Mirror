#region

using System.Collections.Generic;

#endregion

public interface ISaveVersionParser
{
    public LinkedList<int> Parse(string version);
}