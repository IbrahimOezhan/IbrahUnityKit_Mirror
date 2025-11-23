using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace IbrahKit
{
    public class UI_Menu_Item_Button_Text : MonoBehaviour, ISelfValidator
    {
        private UI_Interative_Extension_Text_Modifier text;

        [SerializeField,Required] private UI_Selectable selectable;
        [SerializeField] private UI_Interactive interactive;


        public UnityEvent Initialize(string value)
        {
            interactive.TryGet(out text);

            if (text is UI_Interactive_Extension_Localization local)
            {
                local.SetKey(value);
            }

            if (text is UI_Interative_Extension_Text_Setter setter)
            {
                setter.SetText(value);
            }

            return selectable.GetStateController().GetOnPressSuccess();
        }

        public void Validate(SelfValidationResult result)
        {
            if(interactive == null)
            {
                result.AddError("Interative is required");
                return;
            }

            if(!interactive.TryGet(out UI_Interative_Extension_Text_Modifier modifier))
            {
                result.AddError("Interative needs text modifier");
            }
        }
    }
}