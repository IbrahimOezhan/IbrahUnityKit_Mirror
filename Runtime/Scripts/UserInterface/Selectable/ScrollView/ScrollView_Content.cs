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

    public void Move(float n)
    {
        Vector2 pos = content.anchoredPosition;

        pos.y = n * GetScrollRange();

        content.anchoredPosition = pos;
    }

    public void MoveChildren(Vector2 dif)
    {
        Vector2 pos = content.anchoredPosition;

        pos.y += dif.y;

        pos.y = Mathf.Clamp(pos.y, 0, GetScrollRange());

        content.anchoredPosition = pos;
    }

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

        if (ch <= 0f) return 1f;

        return Mathf.Clamp01(vh / ch);
    }

public float Pos01()
{
    float vh = viewport.rect.height;

    float ch = content.rect.height;

    float range = Mathf.Max(0.0001f, ch - vh);

    // 0 = top, 1 = bottom
    return Mathf.Clamp01(content.anchoredPosition.y / range);
}

}
