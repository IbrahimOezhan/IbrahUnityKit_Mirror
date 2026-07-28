#region

using IbrahKit.Input;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class ScrollView_Controller_Viewport : MonoBehaviour
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
                Vector2 newPos = GetMousePos(scrollView.GetCanvas());

                Vector2 dif = newPos - lastMousePos;

                lastMousePos = newPos;

                scrollView.GetContent().MoveChildren(new Vector2(0f, dif.y));
            }
        }

        private void OnClick()
        {
            lastMousePos = GetMousePos(scrollView.GetCanvas());
            holding = true;
        }

        private void OnClickStop()
        {
            holding = false;
        }

        private Vector2 GetMousePos(Canvas canvas)
        {
            return Cursor_Input_Manager.GetInstance().GetCanvasMousePos(scrollView.GetCanvas());
        }
    }
}