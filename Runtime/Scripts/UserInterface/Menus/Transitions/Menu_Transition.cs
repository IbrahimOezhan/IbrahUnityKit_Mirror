using System.Collections;
using UnityEngine;

namespace IbrahKit
{

    [System.Serializable]
    public abstract class Menu_Transition
    {
        protected UI_Menu menuIn;
        [SerializeField] protected UI_Menu menuOut;

        public Menu_Transition(UI_Menu menuIn, UI_Menu menuOut)
        {
            this.menuIn = menuIn;
            this.menuOut = menuOut;
        }

        public abstract IEnumerator Transition(UI_Menu backOverride);
    }
}