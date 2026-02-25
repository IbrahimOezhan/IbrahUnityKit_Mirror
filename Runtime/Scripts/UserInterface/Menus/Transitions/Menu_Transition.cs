using System.Collections;
using UnityEngine;

namespace IbrahKit.UI
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

        public static void Transition(UI_Menu menuIn, UI_Menu menuOut)
        {
            bool inExists = menuIn != null;
            bool outExists = menuOut != null;

            if (outExists) menuOut.GetVisbilityController().SetActive(true);
            if (outExists) menuOut.GetVisbilityController().SetEnabledAlpha(1);
            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);
            if (outExists) menuOut.GetStateController().SetState(MenuState.ENABLED);

            if (inExists) menuIn.GetVisbilityController().SetActive(false);
            if (inExists) menuIn.GetVisbilityController().SetEnabledAlpha(1);
            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);
            if (inExists) menuIn.GetStateController().SetState(MenuState.DISABLED);
        }

        public IEnumerator Transition(MonoBehaviour mono)
        {
            bool inExists = menuIn != null;
            bool outExists = menuOut != null;

            Debug.Log("Test");

            Debug.Log("In Exists: " + inExists + ", Out Exists: " + outExists );

            yield return mono.StartCoroutine(Transition(inExists, outExists));
        }

        protected abstract IEnumerator Transition(bool inExists, bool outExists);

        public UI_Menu GetIn() => menuIn;
        public UI_Menu GetOut() => menuOut;
    }
}