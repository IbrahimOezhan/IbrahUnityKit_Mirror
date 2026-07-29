using System.Collections.Generic;
using UnityEngine;

public interface ISaveVersionParser
{
    public LinkedList<int> Parse(string version);
}
