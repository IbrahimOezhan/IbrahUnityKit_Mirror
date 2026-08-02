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

        private Vector2 lastMousePos;

        private void Awake()
        {
            selectable.GetStateController().GetOnPressSuccess().AddListener(OnClick);

            selectable.GetStateController().GetOnPressStop().AddListener(OnClickStop);
        }

        private void Update()
        {
            if (!holding) return;

            Vector3[] corners = scrollView.GetTrack().GetCanvasCorners(scrollView.GetCanvas());

            float top = corners[1].y;

            float bottom = corners[0].y;

            Vector2 mp = scrollView.GetMousePos();

            // normalize mouse Y within track: 0=top, 1=bottom
            float n = 1f - Mathf.InverseLerp(bottom, top, mp.y);

            scrollView.GetContent().Move(n);
        }

        private void OnClick()
        {
            lastMousePos = scrollView.GetMousePos();

            Vector3[] corners = scrollView.GetTrack().GetCanvasCorners(scrollView.GetCanvas());

            float top = corners[1].y;

            float btm = corners[0].y;

            float lastMouseY = lastMousePos.y;

            float n = lastMouseY.Normalize(btm, top);

            scrollView.GetContent().Move(n);

            holding = true;
        }

        private void OnClickStop()
        {
            holding = false;
        }
    }
}