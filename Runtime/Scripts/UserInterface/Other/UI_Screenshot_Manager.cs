using IbrahKit;
using IbrahKit.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Screenshot_Manager : Screenshot_Manager
{
    private Action actionScreenshotNoUI;

    [SerializeField] private Key screenshotNoUI;

    protected override void InstanceAwake()
    {
        base.InstanceAwake();

        actionScreenshotNoUI = () => ScreenshotNoResult();

        Input_Shortcut_Manager.GetInstance().RegisterAction(screenshotNoUI, actionScreenshotNoUI);
    }

    protected override void InstanceDestroy()
    {
        base.InstanceDestroy();

        Input_Shortcut_Manager.GetInstance().UnregisterAction(screenshotNoUI, actionScreenshotNoUI);
    }

    private async void ScreenshotNoResult()
    {
        UI_Menu_Manager.GetInstance().Hide();

        await Task.Yield();

        Screenshot();

        await Task.Yield();

        UI_Menu_Manager.GetInstance().Hide();
    }
}
