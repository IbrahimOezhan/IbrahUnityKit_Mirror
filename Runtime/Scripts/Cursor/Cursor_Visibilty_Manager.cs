using IbrahKit;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Visibilty_Manager : MonoBehaviour, IDebug
{
    private bool isVisible;

    private InputType inputType;

    [SerializeField] private List<CursorVisibilty> cursorVisibility;

    public static Cursor_Visibilty_Manager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        Input_Manager.Instance.OnInputChanged += OnInputTypeChanged;
        Input_Manager.Instance.InputUpdate();
    }

    private void Start()
    {
        Debug_Manager.Instance.Add(this);
    }

    private void Update()
    {
        switch (inputType)
        {
            case InputType.KEYBOARD:
            case InputType.MOUSE:

                isVisible = IsVisible(State_Manager.Instance.GetCurrentState());

                break;
            case InputType.GAMEPAD:

                isVisible = false;

                break;
        }
    }

    private void OnDestroy()
    {
        if (Input_Manager.Instance) Input_Manager.Instance.OnInputChanged -= OnInputTypeChanged;
    }

    private bool IsVisible(string state)
    {
        for (int i = 0; i < cursorVisibility.Count; i++)
        {
            if (cursorVisibility[i].Match(state))
            {
                return cursorVisibility[i].State();
            }
        }

        return true;
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    private void OnInputTypeChanged(InputType type)
    {
        inputType = type;
    }

    public string Run()
    {
        return "Cursor Visibilty: " + IsVisible();
    }

    public int Order()
    {
        return 0;
    }

    [System.Serializable]
    private class CursorVisibilty
    {
        [Dropdown("States"),SerializeField] private string state;
        [SerializeField] private bool visible;

        public bool Match(string state)
        {
            return state.Equals(state);
        }

        public bool State()
        {
            return visible;
        }
    }
}