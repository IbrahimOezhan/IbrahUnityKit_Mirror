#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.ScrollView
{
    public class UI_Scroll_View_Content : MonoBehaviour
    {
        [SerializeField, ReadOnly] private RectTransform[] children;

        [SerializeField] private RectTransform content;

        [SerializeField] private RectTransform viewport;

        private void Awake() => UpdateChildren();

        private void OnTransformChildrenChanged() => UpdateChildren();

        private void UpdateChildren()
        {
            int count = content.childCount;

            children = new RectTransform[count];

            for (int i = 0; i < count; i++)
                children[i] = content.GetChild(i) as RectTransform;
        }

        /**
         * Moves absolutely to n (needs to be between 0 and 1)
         */
        public void Move(float n)
        {
            Vector2 pos = content.anchoredPosition;

            pos.y = n * GetScrollRange();

            content.anchoredPosition = pos;
        }

        /**
         * Moves by delta vector
         */
        public void Move(Vector2 dif)
        {
            Vector2 pos = content.anchoredPosition;

            pos.y = Mathf.Clamp(pos.y + dif.y, 0, GetScrollRange());

            content.anchoredPosition = pos;
        }

        /**
         * Returns how much the content can be moved.
         * Example: Viewport is 100 and Content is 300. In that case the maximum aamount that the content can be moved to display
         * everything is 200
         */
        public float GetScrollRange()
        {
            float contentHeight = content.rect.height;

            float viewportHeight = viewport.rect.height;

            float scrollRange = Mathf.Max(0, contentHeight - viewportHeight);

            return scrollRange;
        }

        public float Size01()
        {
            // ratio of visible to total
            float vh = viewport.rect.height;

            float ch = content.rect.height;

            return ch <= 0f ? 1f : Mathf.Clamp01(vh / ch);
        }

        /**
         * Returns a normalized value by how much the content was moved. 0 being top (default position) and 1 being bottom.
         * The Max with 0.0001f is done to prevent dividing by 0
         */
        public float NormalizedScrollAmount()
        {
            float range = GetScrollRange();

            return Mathf.Clamp01(content.anchoredPosition.y / Mathf.Max(0.0001f, range));
        }
    }
}