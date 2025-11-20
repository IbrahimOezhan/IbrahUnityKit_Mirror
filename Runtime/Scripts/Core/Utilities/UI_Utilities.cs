using UnityEngine;

public static class UI_Utilities
{
    public static Vector3[] GetCanvasCorners(this RectTransform transform, Canvas canvas)
    {
        Vector3[] corners = new Vector3[4];

        transform.GetWorldCorners(corners);

        for (int i = 0; i < 4; i++) corners[i] = canvas.transform.InverseTransformPoint(corners[i]);

        return corners;
    }
}
