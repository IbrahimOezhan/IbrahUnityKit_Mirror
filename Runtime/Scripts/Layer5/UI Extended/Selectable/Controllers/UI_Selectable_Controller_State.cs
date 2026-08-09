#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace IbrahKit.UI.Selectable
{
    [Serializable]
    public class UI_Selectable_Controller_State : UI_Selectable_Controller
    {
        public static UI_Selectable_Controller_State currentlySelected;
#if ODIN_INSPECTOR
        [SerializeField, ReadOnly]
#endif
        private UI_Selectable_Group group;

#if ODIN_INSPECTOR
        [SerializeField, ReadOnly]
#endif
        private UI_SELECTABLE_STATE state;

        [SerializeField] private bool interactable = true;

        [SerializeField] private bool playAudioOnStateChange = true;

        [SerializeField] private UnityEvent OnPressedSuccess;

        [SerializeField] private UnityEvent OnPressedFailed;

        [SerializeField] private UnityEvent OnPressedStop;

        private readonly UnityEvent<UI_SELECTABLE_STATE, bool> OnStateChanged = new();

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

            if (!interactable || !playAudioOnStateChange) return;

            Transform transform = GetSelectable().transform;
            
            if (!UI_Audio_Config.TryGet(transform, out UI_Audio_Config result))
            {
                return;
            }
            
            result.OnHover();
        }

        /// <summary>
        ///     Presses the selectable
        /// </summary>
        /// <param name="skipActionsOnPress"></param>
        /// Prevents actions from being invoked on press
        public void Pressed(bool skipActionsOnPress = false)
        {
            SetState(UI_SELECTABLE_STATE.PRESSED);

            currentlySelected = this;

            if (group != null)
            {
                group.OnSelect(GetSelectable());
            }

            if (skipActionsOnPress) return;

            if (!interactable)
            {
                OnPressedFailed.Invoke();
                return;
            }
            
            OnPressedSuccess.Invoke();

            if (!playAudioOnStateChange) return;
                
            if (UI_Audio_Config.TryGet(GetSelectable().transform, out UI_Audio_Config result))
            {
                result.OnClick();
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