#region

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

            selectable.GetStateController().GetOnPressStop().AddListener(OnClickStop);
        }

        private void Update()
        {
            if (!holding) return;

            Vector2 newPos = scrollView.GetMousePos();

            Vector2 dif = newPos - lastMousePos;

            lastMousePos = newPos;

            scrollView.GetContent().MoveChildren(new Vector2(0f, dif.y));
        }

        private void OnClick()
        {
            lastMousePos = scrollView.GetMousePos();

            holding = true;
        }

        private void OnClickStop()
        {
            holding = false;
        }
    }
}