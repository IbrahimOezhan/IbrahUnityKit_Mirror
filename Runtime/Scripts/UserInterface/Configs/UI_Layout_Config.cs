using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UI_Layout_Config : UI_Config
{
    [SerializeField] private List<string> activeLayouts = new();

    public List<string> GetActiveLayouts()
    {
        return activeLayouts;
    }
}
