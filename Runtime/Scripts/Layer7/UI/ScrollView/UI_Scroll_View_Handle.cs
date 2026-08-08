#region

using IbrahKit.UI.Selectable;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.UI.ScrollView
{
    public class UI_Scroll_View_Handle : MonoBehaviour
    {
        [SerializeField] private UI_Scroll_View scrollView;

        [SerializeField] private UI_Selectable selectable;

        private bool holding;

        private void Awake()
        {
            selectable.GetStateController().GetOnPressSuccess().AddListener(MoveOnClick);

            selectable.GetStateController().GetOnPressStop().AddListener(OnClickStop);
        }

        private void Update()
        {
            MoveOnHold();
        }

        private void MoveOnClick()
        {
            Move();

            holding = true;
        }

        private void MoveOnHold()
        {
            if (!holding) return;

            Move();
        }

        private void Move()
        {
            Vector2 mousePos = scrollView.GetMousePos();

            Vector3[] corners = scrollView.GetHandleTrack().GetCanvasCorners(scrollView.GetCanvas());

            float top = corners[1].y;

            float bottom = corners[0].y;

            float scroll = 1f - Mathf.InverseLerp(bottom, top, mousePos.y);

            scrollView.GetContent().Move(scroll);
        }

        public void AdjustHandleToViewport()
        {
            float normalizedScrollAmount = scrollView.GetContent().NormalizedScrollAmount();

            float travel = Mathf.Max(0f, scrollView.GetHandleTrack().rect.height - scrollView.GetHandle().sizeDelta.y);

            Vector2 anchoredPosition = scrollView.GetHandle().anchoredPosition;

            // minus because pivot.y = 1
            anchoredPosition.y = -normalizedScrollAmount * travel;

            scrollView.GetHandle().anchoredPosition = anchoredPosition;
        }

        private void OnClickStop()
        {
            holding = false;
        }
    }
}