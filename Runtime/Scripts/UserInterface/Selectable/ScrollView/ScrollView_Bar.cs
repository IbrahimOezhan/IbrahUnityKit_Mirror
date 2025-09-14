using IbrahKit;
using UnityEngine;
using Debug = IbrahKit.Debug;

public class ScrollView_Bar : MonoBehaviour
{
    private bool holding;
    private Vector2 initialPos;

    [SerializeField] private UI_Selectable selectable;
    [SerializeField] private ScrollView_Content content;
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        selectable.GetOnClick().AddListener(OnClick);
        selectable.GetOnDeSelect().AddListener(OnStopClicking);
    }

    private void OnClick()
    {
        initialPos = Cursor_Input_Manager.Instance.GetCanvasMousePos(canvas);
        holding = true;
        Debug.Log("[ScrollView_Bar] Begin drag");
    }

    private void OnStopClicking()
    {
        holding = false;
        Debug.Log("[ScrollView_Bar] End drag");
    }

    private void Update()
    {
        if (holding)
        {
            Vector2 newPos = Cursor_Input_Manager.Instance.GetCanvasMousePos(canvas);
            Vector2 dif = newPos - initialPos;
            initialPos = newPos;

            // Apply directly
            content.MoveChildren(new Vector2(0f, dif.y)); // only vertical
        }
    }
}
