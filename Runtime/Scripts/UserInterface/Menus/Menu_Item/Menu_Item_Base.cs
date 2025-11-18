using IbrahKit;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Menu_Item_Base
{
    [SerializeField] private bool skip;

    [SerializeField] private bool layoutSpecific;

    [ShowIf(nameof(layoutSpecific)), SerializeField] private List<string> showOnLayouts;

    public bool TrySpawn(RectTransform parent, UI_Menu menu, out GameObject go)
    {
        go = null;

        if (skip)
        {
            IbrahDebug.Log("Skipped");
            return false;
        }

        if (layoutSpecific && (UI_Configs.GetLayout(UI_Configs.GetConfigs(parent), out UI_Layout_Config_SO config) && !UI_Config_Manager.GetInstance().ShowLayout(config, showOnLayouts)))
        {
            IbrahDebug.Log("Skipped due to layout specific");
            return false;
        }

        if (TrySpawnPro(parent, menu, out go)) return true;

        return false;
    }

    protected abstract bool TrySpawnPro(RectTransform parent, UI_Menu menu,out GameObject go);
}
