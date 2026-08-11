#region

using System;
using IbrahKit.Manager;
using IbrahKit.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Input
{
    public class Input_Common_Shortcuts : MonoBehaviourSingletonDontDestroyOnLoad<Input_Common_Shortcuts>
    {
        [SerializeField] private Key screenshot;

        private Action actionScreenshot;

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