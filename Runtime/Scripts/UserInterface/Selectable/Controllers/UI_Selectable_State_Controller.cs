using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class UI_Selectable_State_Controller : UI_Selectable_Controller
    {
#if ODIN_INSPECTOR
        [SerializeField, ReadOnly]
#endif
        private UI_Selectable_Group group;

#if ODIN_INSPECTOR
        [SerializeField, ReadOnly]
#endif
        private UI_SELECTABLE_STATE state;

        [SerializeField]
        private bool interactable = true;

        [SerializeField]
        private bool playAudioOnStateChange = true;

        [SerializeField]
        private UnityEvent OnPressedSuccess;

        [SerializeField]
        private UnityEvent OnPressedFailed;

        [SerializeField]
        private UnityEvent OnPressedStop;

        private readonly UnityEvent<UI_SELECTABLE_STATE, bool> OnStateChanged = new();

        public static UI_Selectable_State_Controller currentlySelected;

        protected override void Init()
        {
            group = GetSelectable().GetGroup();
        }

        public override void OnEnable()
        {

        }

        public override void OnDisable()
        {
            PressedStop(false);
        }

        public void SetState(UI_SELECTABLE_STATE state, bool animate = true)
        {
            if (this.state == state) return;

            if (this.state == UI_SELECTABLE_STATE.PRESSED && state != UI_SELECTABLE_STATE.PRESSED)
            {
                OnPressedStop.Invoke();
            }

            this.state = state;

            OnStateChanged.Invoke(state, animate);
        }

        public void Select()
        {
            SetState(UI_SELECTABLE_STATE.SELECTED);

            if (interactable && playAudioOnStateChange)
            {
                GetSelectable().GetMenu().OnHoverAudio();
            }
        }

        /// <summary>
        /// Presses the selectable
        /// </summary>
        /// <param name="skipActionsOnPress"></param> Prevents actions from being invoked on press
        public void Pressed(bool skipActionsOnPress = false)
        {
            SetState(UI_SELECTABLE_STATE.PRESSED);

            currentlySelected = this;

            if (group != null)
            {
                group.OnSelect(GetSelectable());
            }

            if (skipActionsOnPress) return;

            if (interactable)
            {
                OnPressedSuccess.Invoke();

                if (playAudioOnStateChange) GetSelectable().GetMenu().OnClickAudio();
            }
            else
            {
                OnPressedFailed.Invoke();
            }
        }

        public void PressedStop(bool animate = true)
        {
            SetState(UI_SELECTABLE_STATE.NONE, animate);

            if (currentlySelected == this)
            {
                currentlySelected = null;
            }
        }

        public void SetInteractable(bool value)
        {
            interactable = value;
        }

        public bool GetInteractable() => interactable;

        public UI_SELECTABLE_STATE GetState() => state;

        public UnityEvent<UI_SELECTABLE_STATE, bool> GetOnStateChangedEvent() => OnStateChanged;

        public UnityEvent GetOnPressSuccess() => OnPressedSuccess;

        public UnityEvent GetOnPressFail() => OnPressedFailed;

        public UnityEvent GetOnPressStop() => OnPressedStop;
    }
}