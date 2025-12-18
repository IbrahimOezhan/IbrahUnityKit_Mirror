using IbrahKit.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    public class Visual_Debug_UI_Menu : UI_Menu, ISelfValidator
    {
        private UI_Interative_Extension_Text_Setter textSetter;

        [SerializeField] private UI_Interactive debugContent;

        public override void OnMenuEnabled()
        {
            base.OnMenuEnabled();

            debugContent.TryGet(out textSetter);
        }

        public void Validate(SelfValidationResult result)
        {
            if (debugContent == null)
            {
                result.AddError("Debug Content is null");
                return;
            }

            if (!debugContent.TryGet(out textSetter))
            {
                result.AddError("UI Interactive doesnt contain Text Setter");
            }
        }
    }
}
