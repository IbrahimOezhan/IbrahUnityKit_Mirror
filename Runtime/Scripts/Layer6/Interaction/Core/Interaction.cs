#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace IbrahKit.Interaction
{
    /// <summary>
    ///     An Interaction between a specific player and an object.
    /// </summary>
    public class Interaction
    {
        private readonly List<Interaction_Event> events;
        private readonly Interactable interactable;
        private readonly Interaction_Manager manager;
        private bool isDone;

        public Interaction(Interaction_Manager manager, Interactable interactable, List<Interaction_Event> events)
        {
            this.interactable = interactable;
            this.events = events;
            this.manager = manager;
        }

        public IEnumerator SelectInteraction()
        {
            for (int i = 0; i < events.Count; i++)
            {
                yield return manager.StartCoroutine(events[i].InteractionEventRoutine(interactable));
            }

            EndInteraction();
        }

        public void EndInteraction()
        {
            isDone = true;
        }

        public bool IsDone() => isDone;
    }
}