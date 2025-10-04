using UnityEngine;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.ui)]
    public class UI_Menu_Manager : Manager_DDOL<UI_Menu_Manager>
    {
        public void Transition(Menu_Transition transition, UI_Menu _overrideBackMenu = null)
        {
            if (transition == null)
            {
                Debug.LogWarning("Passed transition is null");
                return;
            }

            StartCoroutine(transition.Transition(_overrideBackMenu));
        }
    }
}