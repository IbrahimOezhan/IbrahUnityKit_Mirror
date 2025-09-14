using IbrahKit;
using UnityEngine;

public class ScrollView_Controller_Viewport : MonoBehaviour
{
    private bool holding;

    private Vector2 lastMousePos;

    [SerializeField] private ScrollView scrollView;

    [SerializeField] private UI_Selectable selectable;

    private void Awake()
    {
        selectable.GetOnClick().AddListener(OnClick);
        selectable.GetOnDeSelect().AddListener(OnClickStop);
    }

    private void Update()
    {
        if (holding)
        {
            Vector2 newPos = GetMousePos(scrollView.GetCanvas());

            Vector2 dif = newPos - lastMousePos;

            lastMousePos = newPos;

            scrollView.GetContent().MoveChildren(new Vector2(0f, dif.y));
        }
    }

    private void OnClick()
    {
        lastMousePos = GetMousePos(scrollView.GetCanvas());
        holding = true;
    }

    private void OnClickStop()
    {
        holding = false;
    }

    private Vector2 GetMousePos(Canvas canvas)
    {
        return Cursor_Input_Manager.Instance.GetCanvasMousePos(scrollView.GetCanvas());
    }
}
