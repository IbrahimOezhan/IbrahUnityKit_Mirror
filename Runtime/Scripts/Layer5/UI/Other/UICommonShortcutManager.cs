#region

using System;
using System.Threading.Tasks;
using IbrahKit.Input;
using IbrahKit.UI;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

public class UICommonShortcutManager : CommonShortcut_Manager
{
    [SerializeField] private Key screenshotNoUI;
    private Action actionScreenshotNoUI;

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