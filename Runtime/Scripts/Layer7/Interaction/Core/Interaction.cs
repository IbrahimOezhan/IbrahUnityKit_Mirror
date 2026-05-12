#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace IbrahKit.Interaction
{
    public class Interaction
    {
        private bool isDone;
        private Interaction_Manager manager;
        private Interactable interactable;
        private List<Interaction_Event_Extension> events;
    
        public Interaction(Interaction_Manager manager,Interactable interactable,List<Interaction_Event_Extension> iEvents )
        {
            this.interactable = interactable;
            this.events = iEvents;
            this.manager = manager;
        }
        
        public IEnumerator SelectInteraction(Interaction_Manager manager)
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
