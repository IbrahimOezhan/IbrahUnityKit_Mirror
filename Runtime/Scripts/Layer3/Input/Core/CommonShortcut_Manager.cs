#region

using System;
using IbrahKit.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Input
{
    public class CommonShortcut_Manager : Manager_Global<CommonShortcut_Manager>
    {
        private Action actionScreenshot;

        [SerializeField] private Key screenshot;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            actionScreenshot = () => Screenshot();

            Input_Shortcut_Manager.GetInstance().RegisterAction(screenshot, actionScreenshot);
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            Input_Shortcut_Manager.GetInstance().UnregisterAction(screenshot, actionScreenshot);
        }

        public void Screenshot()
        {
            Image_Utilities.Screenshot();
        }
    }
}