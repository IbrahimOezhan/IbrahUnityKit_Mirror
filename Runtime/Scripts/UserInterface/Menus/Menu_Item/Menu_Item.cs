using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item
    {
        [SerializeField] private bool skip;

        [SerializeField] private bool layoutSpecific;

        [ShowIf(nameof(layoutSpecific)), SerializeField] private List<string> showOnLayouts;

        [SerializeReference] private Menu_Item_Extension menuItem;

        public bool Spawn(RectTransform parent, UI_Menu menu, out GameObject result)
        {
            result = null;

            if (skip)
            {
                Debug.Log("Skipped");
                return false;
            }

            if (layoutSpecific && (UI_Configs.GetLayout(UI_Configs.GetConfigs(parent), out UI_Layout_Config_SO config) && !UI_Config_Manager.Instance.ShowLayout(config, showOnLayouts)))
            {
                Debug.Log("Skipped due to layout specific");
                return false;
            }

            if (!menuItem.Spawn(parent, menu))
            {
                return false;
            }

            result = menuItem.GetSpawnedObject();
            return true;
        }
    }
}