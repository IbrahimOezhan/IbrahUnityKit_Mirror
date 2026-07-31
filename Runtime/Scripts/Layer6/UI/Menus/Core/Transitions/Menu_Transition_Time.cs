#region

using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    public abstract class Menu_Transition_Time : Menu_Transition
    {
        [SerializeField] protected float time;

        public Menu_Transition_Time(UI_Menu menuIn, UI_Menu menuOut, float time = 1) : base(menuIn, menuOut)
        {
            this.time = time;
        }
    }
}