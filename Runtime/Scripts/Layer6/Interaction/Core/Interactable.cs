#region

using System.Collections.Generic;
using IbrahKit.Localization;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Interaction
{
    /// <summary>
    /// Mono Behavior that holds a list of Events to be called on Interaction
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        private bool canInteract = true;

        private readonly List<Interaction> interactions = new();

        [SerializeField, SerializeReference]
        private List<Interaction_Event> iEvents = new();

        [SerializeField]
        private bool overrideKey;

        [ShowIf(nameof(overrideKey)), SerializeField]
        private Local_Key interactionKey;

        private void OnDestroy()
        {
            interactions.ForEach(x => x.EndInteraction());
        }

        private void OnDisable()
        {
            interactions.ForEach(x => x.EndInteraction());
        }

        public void SetCanInteract(bool value)
        {
            this.canInteract = value;
        }

        public virtual bool CanInteract()
        {
            return canInteract;
        }

        // Interaction manager is passed instead of using the singleton due to the possibility of being able to use two managers for a split screen game
        [Button]
        public Interaction Interact(Interaction_Manager manager)
        {
            Interaction interaction = new(manager, this, iEvents);
            interactions.Add(interaction);
            manager.StartCoroutine(interaction.SelectInteraction());
            return interaction;
        }

        public string OverrideKey(string key)
        {
            return overrideKey? interactionKey : key;
        }
    }
}