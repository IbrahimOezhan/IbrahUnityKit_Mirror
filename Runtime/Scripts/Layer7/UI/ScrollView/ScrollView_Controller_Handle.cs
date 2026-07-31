#region

using IbrahKit.Input;
using IbrahKit.UI;
using IbrahKit.UI.Selectable;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit
{
    public class ScrollView_Controller_Handle : MonoBehaviour
    {
        [SerializeField] private ScrollView scrollView;

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
            if (holding)
            {
                Vector3[] corners = scrollView.GetTrack().GetCanvasCorners(scrollView.GetCanvas());

                float top = corners[1].y;

                float bottom = corners[0].y;

                Vector2 mp = GetMousePos(scrollView.GetCanvas());

                // normalize mouse Y within track: 0=top, 1=bottom
                float n = 1f - Mathf.InverseLerp(bottom, top, mp.y);

                scrollView.GetContent().Move(n);
            }
        }

        private void OnClick()
        {
            lastMousePos = GetMousePos(scrollView.GetCanvas());

            Vector3[] corners = scrollView.GetTrack().GetCanvasCorners(scrollView.GetCanvas());

            float top = corners[1].y;

            float btm = corners[0].y;

            float msy = lastMousePos.y;

            float n = Math_Utilities.Normalize(msy, btm, top);

            scrollView.GetContent().Move(n);

            holding = true;
        }

        private void OnClickStop()
        {
            holding = false;
        }

        private Vector2 GetMousePos(Canvas canvas)
        {
            return Cursor_Input_Manager.GetInstance().GetCanvasMousePos(canvas);
        }
    }
}