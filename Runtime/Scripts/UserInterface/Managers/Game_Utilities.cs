using IbrahKit;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game_Utilities : MonoBehaviour
{
    private bool hidden;

    [SerializeField] private KeyMap keyMap;

    public Action<bool> OnHide;

    public static Game_Utilities Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Keyboard.current[keyMap.screenshot].wasPressedThisFrame)
        {
            Screenshot();
        }
        if (Keyboard.current[keyMap.screenshotNoUI].wasPressedThisFrame)
        {
            ScreenshotNoUI();
        }
        if (Keyboard.current[keyMap.hideUI].wasPressedThisFrame)
        {
            Hide();
        }
    }

    public void Screenshot()
    {
        Basic_Utilities.Screenshot();
    }

    public void ScreenshotNoUI()
    {
        ScreenshotNoResult();
    }

    private async void ScreenshotNoResult()
    {
        Hide();

        await Task.Yield();

        Basic_Utilities.Screenshot();

        await Task.Yield();

        Hide();
    }

    public void Hide()
    {
        hidden = !hidden;
        UpdateHide();
    }

    public void UpdateHide()
    {
        OnHide?.Invoke(hidden);
    }
}