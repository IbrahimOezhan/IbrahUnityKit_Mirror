using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.ui)]
    public class UI_Menu_Manager : Manager_DDOL<UI_Menu_Manager>
    {
        [SerializeField] private List<UI_Menu> activeMenus = new();

        public void Transition(Menu_Transition transition, UI_Menu _overrideBackMenu = null)
        {
            Debug.Log("Enable2");

            StartCoroutine(transition.Transition(_overrideBackMenu));
        }
    }
}