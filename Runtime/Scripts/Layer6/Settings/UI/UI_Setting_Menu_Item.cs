#region

using System;
using IbrahKit.Settings;
using IbrahKit.UI.Menu;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Setting_Menu_Item : Menu_Item_Base
    {
        [SerializeField] private UI_Setting ui;

        [SerializeField] public Setting_Config config;

        protected override bool Spawn(RectTransform parent, UI_Menu menu, out GameObject go)
        {
            UI_Setting setting = Object.Instantiate(ui, parent);

            Settings_Manager.GetInstance().TryGet(config.GetKey(), out Setting s);

            setting.Init(s);

            go = setting.gameObject;

            return false;
        }
    }
}