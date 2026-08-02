#region

using IbrahKit.Input;
using UnityEngine;

#endregion

namespace IbrahKit.UI.ScrollView
{
    public class UI_Scroll_View : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        [SerializeField] private UI_Scroll_View_Content content;

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

        public UI_Scroll_View_Content GetContent()
        {
            return content;
        }

        public RectTransform GetTrack()
        {
            return track;
        }

        public Vector2 GetMousePos() => Cursor_Input_Manager.GetInstance().GetCanvasMousePos(GetCanvas());
    }
}