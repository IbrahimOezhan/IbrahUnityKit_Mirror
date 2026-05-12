#region

using System;
using System.Collections;
using IbrahKit.Debugging;

#endregion

namespace IbrahKit.Interaction
{
    [Serializable]
    public class Interaction_Event_Interface : Interaction_Event_Extension
    {
        public override IEnumerator InteractionEventRoutine(Interactable interactable)
        {
            if (interactable.TryGetComponent(out IInteractable iface))
            {
                iface.OnInteract(interactable);
            }
            else
            {
                IbrahDebug.LogWarning("No interface of type IInteractable found");
            }

            yield return null;
        }
    }
}