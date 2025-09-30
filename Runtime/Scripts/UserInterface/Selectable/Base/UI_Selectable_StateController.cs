using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Selectable_StateController
    {
        [SerializeField, ReadOnly]
        private UI_Selectable selectable;

        [SerializeField, ReadOnly]
        private UI_Selectable_Group group;

        [SerializeField, ReadOnly]
        private UI_SELECTABLE_STATE state;

        [SerializeField]
        private bool interactable = true;

        [SerializeField]
        private UnityEvent OnPressedSuccess;

        [SerializeField]
        private UnityEvent OnPressedFailed;

        [SerializeField]
        private UnityEvent OnPressedStop;

        private UnityEvent<UI_SELECTABLE_STATE> OnStateChanged = new();

        public static UI_Selectable currentlySelected;

        public void Init(UI_Selectable selectable, UI_Selectable_Group group)
        {
            this.selectable = selectable;

            this.group = group;
        }

        public void SetState(UI_SELECTABLE_STATE state)
        {
            if (this.state == state) return;

            if (this.state == UI_SELECTABLE_STATE.PRESSED && state != UI_SELECTABLE_STATE.PRESSED)
            {
                OnPressedStop.Invoke();
            }

            this.state = state;

            OnStateChanged.Invoke(state);
        }

        public UI_SELECTABLE_STATE GetState()
        {
            return this.state;
        }

        public void Select()
        {
            SetState(UI_SELECTABLE_STATE.SELECTED);

            if (interactable)
            {
                selectable.GetParentMenu().OnHoverAudio();
            }
        }

        public void Pressed()
        {
            SetState(UI_SELECTABLE_STATE.PRESSED);

            currentlySelected = selectable;

            if (group != null)
            {
                group.OnSelect(selectable);
                Debug.Log("Group On Select");
            }

            if (interactable)
            {
                OnPressedSuccess.Invoke();

                selectable.GetParentMenu().OnClickAudio();
            }
            else
            {
                OnPressedFailed.Invoke();
            }
        }

        public void PressedStop()
        {
            SetState(UI_SELECTABLE_STATE.NONE);

            if (currentlySelected == selectable)
            {
                currentlySelected = null;
            }
        }

        public void SetInteractable(bool value)
        {
            interactable = value;
        }

        public bool GetInteractable() => interactable;

        public UnityEvent<UI_SELECTABLE_STATE> GetOnStateChangedEvent() => OnStateChanged;
        public UnityEvent GetOnPressSuccess() => OnPressedSuccess;
        public UnityEvent GetOnPressFail() => OnPressedFailed;
        public UnityEvent GetOnPressStop() => OnPressedStop;
    }
}