using IbrahKit;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Selectable_CursorInput : IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private UI_Selectable_StateController stateController;
    private UI_Selectable_Group group;

    public void Init(UI_Selectable_StateController stateController,UI_Selectable_Group group)
    {
        this.stateController = stateController;
        this.group = group;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (group != null && stateController.GetState() == UI_SELECTABLE_STATE.PRESSED) return;

        stateController.Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (group != null && stateController.GetState() == UI_SELECTABLE_STATE.PRESSED) return;

        stateController.PressedStop();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (group != null && stateController.GetState() == UI_SELECTABLE_STATE.PRESSED) return;

        stateController.Pressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (group != null) return;

        stateController.PressedStop();
    }
}
