#region

using IbrahKit.UI.Core.Config;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [CreateAssetMenu(fileName = "NewUIMenuConfig", menuName = "IbrahKit/UI/Menu/MenuConfig")]
    public class UI_Menu_Config : UI_Config<UI_Menu_Config>
    {
        [SerializeField] private Menu_Item_Button menuButtonPrefab;

        [SerializeField] private Menu_Item_Button staticMenuButtonPrefab;

        public Menu_Item_Button GetMenuButton() => menuButtonPrefab;

        public Menu_Item_Button GetMenuButtonStatic() => staticMenuButtonPrefab;
    }
}