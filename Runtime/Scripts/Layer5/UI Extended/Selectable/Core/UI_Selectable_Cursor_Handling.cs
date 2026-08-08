#region

using IbrahKit.Input.Cursor;
using UnityEngine.EventSystems;

#endregion

namespace IbrahKit.UI.Selectable
{
    public partial class UI_Selectable : ICursorReceiver
    {
        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) cursorInput.OnPointerDown(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) cursorInput.OnPointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) cursorInput.OnPointerExit(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) cursorInput.OnPointerUp(eventData);
        }
    }
}