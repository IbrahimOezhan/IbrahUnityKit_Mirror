#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace IbrahKit.Interaction
{
    public class Interaction
    {
        private bool isDone;
        private readonly Interaction_Manager manager;
        private readonly Interactable interactable;
        private readonly List<Interaction_Event_Extension> events;
    
        public Interaction(Interaction_Manager manager,Interactable interactable,List<Interaction_Event_Extension> events)
        {
            this.interactable = interactable;
            this.events = events;
            this.manager = manager;
        }
        
        public IEnumerator SelectInteraction()
        {
            manager.Register(interactable);

            for (int i = 0; i < events.Count; i++)
            {
                yield return manager.StartCoroutine(events[i].InteractionEventRoutine(interactable));
            }

            EndInteraction();
        }
    
        public void EndInteraction()
        {
            manager.Unregister(interactable);
            
            isDone = true;
        }

        public bool IsDone() => isDone;
    }
}
