#region

using System.Linq;
using UnityEngine.EventSystems;

#endregion

namespace IbrahKit.Input.Cursor
{
    /// <summary>
    ///     An interface signalizing the custom cursor that its on top of UI.
    ///     Intercepts the pointer callbacks and only forwards them when mouse input is enabled.
    /// </summary>
    public interface ICursorReceiver : IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
        IPointerExitHandler, IPointerEnterHandler
    {
        private static bool MouseEnabled =>
            Input_Manager.GetInstance().GetManagerData().EnabledInputMethods()
                .Contains(Input_Manager.InputType.MOUSE);

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (MouseEnabled) OnPointerClick(eventData);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (MouseEnabled) OnPointerDown(eventData);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (MouseEnabled) OnPointerEnter(eventData);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (MouseEnabled) OnPointerExit(eventData);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (MouseEnabled) OnPointerUp(eventData);
        }

        new void OnPointerClick(PointerEventData eventData);

        new void OnPointerDown(PointerEventData eventData);

        new void OnPointerUp(PointerEventData eventData);

        new void OnPointerEnter(PointerEventData eventData);

        new void OnPointerExit(PointerEventData eventData);
    }
}