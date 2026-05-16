#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Menu_Item_Button_Text : MonoBehaviour, ISelfValidator
    {
        private UI_Interactive_Extension_Text_Modifier text;

        [SerializeField, Required] private UI_Selectable selectable;

        [SerializeField] private UI_Interactive interactive;

        public void Initialize(string value)
        {
            interactive.TryGetExtension(out text);

            if (text is UI_Interactive_Extension_Localization local)
            {
                local.SetKey(value);
            }

            if (text is UI_Interactive_Extension_Text_Setter setter)
            {
                setter.SetText(value);
            }
        }

        public UI_Selectable GetSelectable() => selectable;

        public void Validate(SelfValidationResult result)
        {
            if (interactive == null)
            {
                result.AddError("Interative is required");
                return;
            }

            if (!interactive.TryGetExtension(out UI_Interactive_Extension_Text_Modifier _))
            {
                result.AddError("Interative needs text modifier");
            }
        }
    }
}