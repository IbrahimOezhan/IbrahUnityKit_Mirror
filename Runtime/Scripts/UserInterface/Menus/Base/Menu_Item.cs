using log4net.Util;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public partial class Menu_Item
    {
        [SerializeField] private bool skip;

        [SerializeField] private bool layoutSpecific;

        [ShowIf(nameof(layoutSpecific)), SerializeField] private List<string> showOnLayouts;

        [SerializeReference] private Menu_Item_Base menuItem;

        public GameObject Spawn(RectTransform parent, UI_Menu menu)
        {
            if (skip)
            {
                Debug.Log("Skipped");
                return null;
            }

            if (layoutSpecific && (UI_Configs.GetLayout(UI_Configs.GetConfigs(parent), out UI_Layout_Config_SO result) && !UI_Config_Manager.Instance.ShowLayout(result,showOnLayouts)))
            {
                Debug.Log("Skipped due to layout specific");
                return null;
            }

            menuItem.Spawn(parent, menu);

            return menuItem.GetSpawnedObject();
        }
    }
}