#region

using System;
using IbrahKit.Interaction;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Interaction_Menu : UI_Menu
    {
        [FoldoutGroup("UI"), SerializeField] private UI_Modifier textInteract;

        [SerializeField] private Interaction_Manager manager;
        private UI_Modifier_Extension_Localization localization;

        private Interaction_Manager.InteractionMachineState state;

        protected override void Awake()
        {
            base.Awake();

            if (!textInteract.TryGetExtension(out localization))
            {
                throw new NullReferenceException("UI_Interactive doesnt contain Localization Component");
            }

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
                localization.SetKey(key);
            }
            else
            {
                localization.SetKey("");
            }
        }
    }
}