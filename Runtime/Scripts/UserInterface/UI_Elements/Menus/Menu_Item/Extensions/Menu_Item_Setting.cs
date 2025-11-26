using IbrahKit.Settings;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item_Setting : Menu_Item_Base
    {
        [SerializeField] private Setting_Map_Element config;

        protected override bool TrySpawnPro(RectTransform parent, UI_Menu menu, out GameObject go)
        {
            if (config.TryCreateUserInterface(Vector3.zero, Quaternion.identity, parent, out UI_Setting res))
            {
                go = res.gameObject;
                return true;
            }

            go = null;
            return false;
        }
    }
}