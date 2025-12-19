using IbrahKit.Debugging;
using System;
using UnityEngine;

namespace IbrahKit.UI
{
    [DefaultExecutionOrder(Execution_Order.ui)]
    public class UI_Menu_Manager : Manager_Global<UI_Menu_Manager, UI_Menu_Manager_Data>
    {
        private bool hidden;

        private Action actionHide;

        public Action<bool> OnHide;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            actionHide = () => Hide();

            Input_Shortcut_Manager.GetInstance().RegisterAction(GetManagerData().GetKey(), actionHide);
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            Input_Shortcut_Manager.GetInstance().UnregisterAction(GetManagerData().GetKey(), actionHide);
        }

        public void Transition(Menu_Transition transition, UI_Menu _overrideBackMenu = null)
        {
            if (transition == null)
            {
                IbrahDebug.LogWarning("Passed transition is null");
                return;
            }

            StartCoroutine(transition.Transition(_overrideBackMenu));
        }

        public void Hide()
        {
            hidden = !hidden;
            InvokeHide();
        }

        public void InvokeHide()
        {
            OnHide?.Invoke(hidden);
        }
    }
}