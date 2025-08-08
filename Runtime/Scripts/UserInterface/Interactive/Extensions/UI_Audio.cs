using UnityEngine.EventSystems;

namespace IbrahKit
{
    [System.Serializable]
    public abstract class UI_Audio : UI_Extension, IPointerDownHandler, IPointerEnterHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHover();
        }

        protected virtual void OnClick()
        {

        }

        protected virtual void OnHover()
        {

        }
    }
}