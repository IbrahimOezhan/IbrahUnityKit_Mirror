using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
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

            if (layoutSpecific &&
                ((UI_Configs.TryGet<UI_Layout_Config_Override, UI_Layout_Config_SO, UI_Layout_Config>(UI_Configs.GetConfigs(parent), out UI_Layout_Config result)
                && !UI_Config_Manager.GetInstance().ShowLayout(result, showOnLayouts))))
            {
                IbrahDebug.Log("Skipped due to layout specific");
                return false;
            }

            if (TrySpawnPro(parent, menu, out go)) return true;

            return false;
        }

        protected abstract bool TrySpawnPro(RectTransform parent, UI_Menu menu, out GameObject go);
    }
}