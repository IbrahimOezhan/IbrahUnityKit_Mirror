using UnityEngine.EventSystems;

namespace IbrahKit.UI
{
    public class UI_Selectable_Input_Cursor_Controller : UI_Selectable_Controller, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private UI_Selectable_State_Controller stateController;

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