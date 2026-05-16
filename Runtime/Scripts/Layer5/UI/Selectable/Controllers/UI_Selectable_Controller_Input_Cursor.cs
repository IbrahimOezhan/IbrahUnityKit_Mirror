#region

using UnityEngine.EventSystems;

#endregion

namespace IbrahKit.UI
{
    public class UI_Selectable_Controller_Input_Cursor : UI_Selectable_Controller
    {
        private UI_Selectable_Controller_State stateController;

        protected override void Init()
        {
            stateController = GetSelectable().GetStateController();
        }

        public override void OnEnable()
        {
        }

        public override void OnDisable()
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (GetSelectable().DisallowPress()) return;

            stateController.Select();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (GetSelectable().DisallowPress()) return;

            stateController.PressedStop();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GetSelectable().DisallowPress()) return;

            stateController.Pressed();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (GetSelectable().DisallowPressOnUp()) return;

            stateController.PressedStop();
        }
    }
}