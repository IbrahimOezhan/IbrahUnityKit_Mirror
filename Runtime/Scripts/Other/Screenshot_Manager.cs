using IbrahKit.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit
{
    public class Screenshot_Manager : Manager_Global<Screenshot_Manager>
    {
        private Action actionScreenshot;

        private Action actionScreenshotNoUI;

        [SerializeField] private Key screenshot;

        [SerializeField] private Key screenshotNoUI;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            actionScreenshot = () => Screenshot();

            actionScreenshotNoUI = () => ScreenshotNoUI();

            Input_Shortcut_Manager.GetInstance().RegisterAction(screenshot, actionScreenshot);

            Input_Shortcut_Manager.GetInstance().RegisterAction(screenshotNoUI, actionScreenshotNoUI);
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            Input_Shortcut_Manager.GetInstance().UnregisterAction(screenshot, actionScreenshot);

            Input_Shortcut_Manager.GetInstance().UnregisterAction(screenshotNoUI, actionScreenshotNoUI);
        }

        public void Screenshot()
        {
            Image_Utilities.Screenshot();
        }

        public void ScreenshotNoUI()
        {
            ScreenshotNoResult();
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