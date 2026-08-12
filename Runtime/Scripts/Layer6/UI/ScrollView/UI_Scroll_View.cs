#region

using IbrahKit.Input.Cursor;
using UnityEngine;

#endregion

namespace IbrahKit.UI.ScrollView
{
    public class UI_Scroll_View : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        [SerializeField] private UI_Scroll_View_Content content;

        [SerializeField] private UI_Scroll_View_Handle handleRef;

        [SerializeField] private RectTransform handleTrack;

        [SerializeField] private RectTransform handle;

        [SerializeField] private RectTransform canvasRect;

        public RectTransform GetCanvasRect() => canvasRect;

        public Canvas GetCanvas() => canvas;

        public UI_Scroll_View_Content GetContent() => content;

        public RectTransform GetHandleTrack() => handleTrack;

        public RectTransform GetHandle() => handle;

        public UI_Scroll_View_Handle GetHandleRef() => handleRef;

        public Vector2 GetMousePos() => Cursor_Manager.GetInstance().GetCursorInput().GetCanvasMousePos(GetCanvas());
    }
}