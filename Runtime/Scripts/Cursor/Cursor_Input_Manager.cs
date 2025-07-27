using IbrahKit;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Cursor_Input_Manager : MonoBehaviour
{
    private CursorInput input;

    private Vector2 mousePos;

    public Action OnLMB;

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

        input.Map.LMB.performed += LMB;

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
            input.Map.LMB.performed -= LMB;

            input.Disable();
            input.Dispose();
        }
    }

    public Vector2 GetMousePos()
    {
        return mousePos;
    }

    public void LMB(InputAction.CallbackContext context)
    {
        OnLMB?.Invoke();
    }

    public bool CursorOverUI(EventSystem system)
    {
        PointerEventData pointerData = new(system)
        {
            position = mousePos
        };

        List<RaycastResult> results = new();

        system.RaycastAll(pointerData, results);

        return results.Where(x => x.gameObject.GetComponent<ICursorHandler>() != null).Count() > 0;
    }
}
