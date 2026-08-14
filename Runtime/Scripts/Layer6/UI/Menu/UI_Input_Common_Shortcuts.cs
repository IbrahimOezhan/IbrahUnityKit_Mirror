#region

using System;
using System.Threading.Tasks;
using IbrahKit.Input;
using IbrahKit.Manager;
using IbrahKit.UI.Menu;
using IbrahKit.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.UI.Other
{
    public class UI_Input_Common_Shortcuts : MonoBehaviourSingletonDontDestroyOnLoad<UI_Input_Common_Shortcuts>
    {
        [SerializeField] private Key screenshotNoUI;
    
        private Action actionScreenshotNoUI;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            Input_Shortcut_Manager.GetInstance().RegisterAction(screenshotNoUI, ScreenshotNoResult);
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            Input_Shortcut_Manager.GetInstance().UnregisterAction(screenshotNoUI, ScreenshotNoResult);
        }

        private async void ScreenshotNoResult()
        {
            UI_Menu_Manager.GetInstance().Hide();

            await Task.Yield();

            Image_Utilities.Screenshot();

            await Task.Yield();

            UI_Menu_Manager.GetInstance().Hide();
        }
    }
}

