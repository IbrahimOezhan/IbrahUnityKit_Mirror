using IbrahKit;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Visibilty_Manager : Manager_Base<Cursor_Visibilty_Manager>, IDebug
{
    private bool isVisible;

    private InputType inputType;

    [SerializeField] private List<CursorVisibilty> cursorVisibility;

    protected override void OnAwake()
    {
        base.OnAwake();

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

                string state = string.Empty;

                if (State_Manager.Instance != null)
                {
                    state = State_Manager.Instance.GetCurrentState();
                }

                isVisible = IsVisible(state);

                break;

            default:

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
            if (cursorVisibility[i].Match(state, out bool res))
            {
                return res;
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
        [Dropdown(State_Manager.KEY), SerializeField] private string state;
        [SerializeField] private bool visible;

        public bool Match(string state, out bool result)
        {
            result = visible;

            return this.state.Equals(state);
        }
    }
}