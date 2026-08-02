#region

using UnityEngine;

#endregion

namespace IbrahKit.UI.ScrollView
{
    public class ScrollView_Handle : MonoBehaviour
    {
        [SerializeField] private UI_Scroll_View_Content content;

        [Header("Scrollbar Visuals")] [SerializeField]
        private RectTransform track;

        [SerializeField] private RectTransform handle;

        private void Update()
        {
            UpdateHandle();
        }

        private void UpdateHandle()
        {
            float pos = content.Pos01(); // 0=top, 1=bottom

            float travel = Mathf.Max(0f, track.rect.height - handle.sizeDelta.y);

            Vector2 ap = handle.anchoredPosition;

            ap.y = -pos * travel; // minus because pivot.y = 1

            handle.anchoredPosition = ap;
        }
    }
}