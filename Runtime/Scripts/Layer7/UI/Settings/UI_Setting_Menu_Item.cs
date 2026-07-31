#region

using System;
using IbrahKit.Settings;
using IbrahKit.UI.Menu;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Setting_Menu_Item : Menu_Item_Base
    {
        [SerializeField] private UI_Setting_Map_Element config;

        protected override bool Spawn(RectTransform parent, UI_Menu menu, out GameObject go)
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