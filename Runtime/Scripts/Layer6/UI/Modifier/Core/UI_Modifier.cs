#region

using IbrahKit.Extension;
using IbrahKit.UI.Menu;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Modifier
{
    public class UI_Modifier : Extension_Handler<UI_Modifier_Extension>, IMenuInit, ISelfValidator
    {
        [SerializeField, ReadOnly] private UI_Menu menu;

        protected void OnDisable()
        {
            Cleanup();
        }

        public void OnMenuInit(UI_Menu menu)
        {
            this.menu = menu;

            InitExtensions();

            RunExtensions();
        }

        public void Validate(SelfValidationResult result)
        {
            foreach (UI_Modifier_Extension extension in GetExtensions())
            {
                extension.Validate(result, gameObject);
            }
        }

        public UI_Menu GetMenu()
        {
            return menu;
        }
    }
}