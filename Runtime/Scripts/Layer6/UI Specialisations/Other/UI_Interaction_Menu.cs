#region

using IbrahKit.Interaction;
using IbrahKit.UI.Menu;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Interaction_Menu : UI_Menu
    {
        [SerializeField] private Interaction_Manager manager;

        private UI_Modifier_Text_Modifier localization;

        private Interaction_Manager.InteractionMachineState state;

        protected override void Awake()
        {
            base.Awake();

            manager.GetStateMachine().stateChanged += OnInteractionStateChanged;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            manager.GetStateMachine().stateChanged -= OnInteractionStateChanged;
        }

        private void OnInteractionStateChanged(Interaction_Manager.InteractionMachineState oldState,
            Interaction_Manager.InteractionMachineState newState)
        {
            this.state = newState;
            UpdateUI();
        }

        private void UpdateUI()
        {
            base.MenuLifecycle();

            if (state is Interaction_Manager.InteractionStateNone interacting)
            {
                string key = interacting.GetInteractable().OverrideKey(manager.GetInteractionKey());
                localization.GetLocalization().SetKey(key);
            }
            else
            {
                localization.GetLocalization().SetKey("");
            }
        }
    }
}