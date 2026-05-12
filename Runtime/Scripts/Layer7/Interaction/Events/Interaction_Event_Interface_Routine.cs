#region

using System;
using System.Collections;
using IbrahKit.Debugging;

#endregion

namespace IbrahKit.Interaction
{
    [Serializable]
    public class Interaction_Event_Interface_Routine : Interaction_Event_Extension
    {
        public override IEnumerator InteractionEventRoutine(Interactable interactable)
        {
            if (interactable.TryGetComponent(out IInteractable iface))
            {
                yield return interactable.StartCoroutine(iface.OnInteractRoutine(interactable));
            }
            else
            {
                IbrahDebug.LogWarning("No interface of type IInteractable found");
            }
        }
    }
}