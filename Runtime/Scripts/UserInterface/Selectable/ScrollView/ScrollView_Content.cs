using Sirenix.OdinInspector;
using UnityEngine;

public class ScrollView_Content : MonoBehaviour
{
    [SerializeField, ReadOnly] private RectTransform[] children;
    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform viewport; // needed for clamp

    private void Awake() => UpdateChildren();
    private void OnTransformChildrenChanged() => UpdateChildren();

    private void UpdateChildren()
    {
        int count = content.childCount;
        children = new RectTransform[count];
        for (int i = 0; i < count; i++)
            children[i] = content.GetChild(i) as RectTransform;
    }

    public void MoveChildren(Vector2 dif)
    {
        Vector2 pos = content.anchoredPosition;
        pos.y += dif.y; // vertical only

        // --- clamp here ---
        float contentHeight = content.rect.height;
        float viewportHeight = viewport.rect.height;
        float scrollRange = Mathf.Max(0, contentHeight - viewportHeight);

        pos.y = Mathf.Clamp(pos.y, 0, scrollRange);

        content.anchoredPosition = pos;
        Debug.Log($"[ScrollView_Content] delta:{dif.y:F1} newY:{pos.y:F1} range:{scrollRange:F1}");
    }
}
