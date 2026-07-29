using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SimpleSaveVersionParser : ISaveVersionParser
{
    public LinkedList<int> Parse(string version)
    {
        LinkedList<int> result = new LinkedList<int>();
        
        MatchCollection m = Regex.Matches(version, "\\d+");

        foreach (Match o in m)
        {
            result.AddLast(int.Parse(o.Value));
        }
        
        return result;
    }
}
