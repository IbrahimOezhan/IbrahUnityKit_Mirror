using UnityEngine;

public class Cursor_Input_Manager : MonoBehaviour
{
    private CursorInput input;

    private Vector2 mousePos;

    public static Cursor_Input_Manager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        input = new();

        input.Enable();

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (input == null)
        {
            return;
        }

        mousePos = input.Map.MousePos.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        if (input != null)
        {
            input.Disable();
            input.Dispose();
        }
    }

    public Vector2 GetMousePos()
    {
        return mousePos;
    }
}
