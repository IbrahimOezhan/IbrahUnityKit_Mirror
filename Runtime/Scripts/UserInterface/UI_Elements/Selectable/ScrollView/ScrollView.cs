using UnityEngine;

namespace IbrahKit
{
    public class ScrollView : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        [SerializeField] private ScrollView_Content content;

        [SerializeField] private RectTransform track;

        [SerializeField] private RectTransform canvasRect;

        public RectTransform GetCanvasRect()
        {
            return canvasRect;
        }

        public Canvas GetCanvas()
        {
            return canvas;
        }

        public ScrollView_Content GetContent()
        {
            return content;
        }

        public RectTransform GetTrack()
        {
            return track;
        }
    }
}