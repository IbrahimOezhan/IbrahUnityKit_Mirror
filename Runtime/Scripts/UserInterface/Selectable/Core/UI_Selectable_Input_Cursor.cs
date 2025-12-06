using UnityEngine.EventSystems;

namespace IbrahKit.UI
{
    public class UI_Selectable_Input_Cursor : IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private UI_Selectable_StateController stateController;
        private UI_Selectable selectable;

        public void Init(UI_Selectable_StateController stateController, UI_Selectable selectable)
        {
            this.stateController = stateController;
            this.selectable = selectable;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (selectable.DisallowPress()) return;

            stateController.Select();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (selectable.DisallowPress()) return;

            stateController.PressedStop();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (selectable.DisallowPress()) return;

            stateController.Pressed();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (selectable.DisallowPressOnUp()) return;

            stateController.PressedStop();
        }
    }
}