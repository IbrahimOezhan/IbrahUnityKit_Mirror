#region

using UnityEngine;

#endregion

namespace IbrahKit.Utilities
{
    public static class UI_Utilities
    {
        public static Vector2 GetCenter(this RectTransform rt, Canvas canvas)
        {
            var c = rt.GetCanvasCorners(canvas);
            return (c[0] + c[2]) * 0.5f;
        }

        public static Vector2 GetLeftEdgeCenter(this RectTransform rt, Canvas canvas)
        {
            var c = rt.GetCanvasCorners(canvas);
            return (c[0] + c[1]) * 0.5f;
        }

        public static Vector2 GetRightEdgeCenter(this RectTransform rt, Canvas canvas)
        {
            var c = rt.GetCanvasCorners(canvas);
            return (c[2] + c[3]) * 0.5f;
        }

        public static Vector2 GetTopEdgeCenter(this RectTransform rt, Canvas canvas)
        {
            var c = rt.GetCanvasCorners(canvas);
            return (c[1] + c[2]) * 0.5f;
        }

        public static Vector2 GetBottomEdgeCenter(this RectTransform rt, Canvas canvas)
        {
            var c = rt.GetCanvasCorners(canvas);
            return (c[0] + c[3]) * 0.5f;
        }


        public static float GetLeftX(this RectTransform rt, Canvas canvas)
        {
            var c = rt.GetCanvasCorners(canvas);
            return c[0].x; // bottom-left
        }

        public static float GetRightX(this RectTransform rt, Canvas canvas)
        {
            var c = rt.GetCanvasCorners(canvas);
            return c[2].x; // top-right
        }

        public static float GetBottomY(this RectTransform rt, Canvas canvas)
        {
            var c = rt.GetCanvasCorners(canvas);
            return c[0].y; // bottom-left
        }

        public static float GetTopY(this RectTransform rt, Canvas canvas)
        {
            var c = rt.GetCanvasCorners(canvas);
            return c[1].y; // top-left
        }


        public static Vector3[] GetCanvasCorners(this RectTransform transform, Canvas canvas)
        {
            Vector3[] corners = new Vector3[4];

            transform.GetWorldCorners(corners);

            for (int i = 0; i < 4; i++) corners[i] = canvas.transform.InverseTransformPoint(corners[i]);

            return corners;
        }
    }
}