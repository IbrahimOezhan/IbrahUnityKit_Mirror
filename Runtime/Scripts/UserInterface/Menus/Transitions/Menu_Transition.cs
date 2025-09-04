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

        public void TransitionBackup()
        {
            bool inExists = menuIn != null;
            bool outExists = menuOut != null;

            if (outExists) menuOut.GetVisbilityController().SetActive(true);
            if (outExists) menuOut.GetVisbilityController().SetAlpha(1);
            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);

            if (inExists) menuIn.GetVisbilityController().SetActive(false);
            if (inExists) menuIn.GetVisbilityController().SetAlpha(1);
            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);
        }

        public abstract IEnumerator Transition(UI_Menu backOverride);
    }
}