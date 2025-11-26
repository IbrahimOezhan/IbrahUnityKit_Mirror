using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit
{
    public class Game_Utilities : Manager_DDOL<Game_Utilities>
    {
        private bool hidden;

        private Action actionScreenshot;
        private Action actionScreenshotNoUI;
        private Action actionHide;

        [SerializeField] private Key screenshot;
        [SerializeField] private Key screenshotNoUI;
        [SerializeField] private Key hideUI;

        public Action<bool> OnHide;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            actionScreenshot = () => Screenshot();
            actionScreenshotNoUI = () => ScreenshotNoUI();
            actionHide = () => Hide();

            Input_Shortcut_Manager.GetInstance().RegisterAction(screenshot, actionScreenshot);
            Input_Shortcut_Manager.GetInstance().RegisterAction(screenshotNoUI, actionScreenshotNoUI);
            Input_Shortcut_Manager.GetInstance().RegisterAction(hideUI, actionHide);
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            Input_Shortcut_Manager.GetInstance().UnregisterAction(screenshot, actionScreenshot);
            Input_Shortcut_Manager.GetInstance().UnregisterAction(screenshotNoUI, actionScreenshotNoUI);
            Input_Shortcut_Manager.GetInstance().UnregisterAction(hideUI, actionHide);
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
            Hide();

            await Task.Yield();

            Image_Utilities.Screenshot();

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
}