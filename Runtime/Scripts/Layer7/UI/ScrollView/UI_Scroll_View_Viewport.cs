#region

using IbrahKit.Input.Cursor;
using IbrahKit.UI.Selectable;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.ScrollView
{
    public class UI_Scroll_View_Viewport : MonoBehaviour
    {
        [SerializeField] private UI_Scroll_View scrollView;

        [SerializeField] private UI_Selectable selectable;

        [SerializeField, ReadOnly] private bool holding;

        [SerializeField, ReadOnly] private Vector2 lastMousePos;

        private void Awake()
        {
            selectable.GetStateController().GetOnPressSuccess().AddListener(OnClick);
        }

        private void Update()
        {
            if (!holding) return;

            if (Cursor_Input_Manager.GetInstance().GetLMB().WasReleasedThisFrame())
            {
                holding = false;
            }

            Vector2 newPos = scrollView.GetMousePos();

            Vector2 dif = newPos - lastMousePos;

            Vector2 delta = new Vector2(0f, dif.y);

            lastMousePos = newPos;

            scrollView.GetContent().MoveChildren(delta);
        }

        private void OnClick()
        {
            lastMousePos = scrollView.GetMousePos();

            holding = true;
        }
    }
}