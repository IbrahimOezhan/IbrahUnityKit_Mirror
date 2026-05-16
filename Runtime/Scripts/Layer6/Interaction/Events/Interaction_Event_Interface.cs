#region

using System;
using System.Collections;
using IbrahKit.Debugging;

#endregion

namespace IbrahKit.Interaction
{
    /// <summary>
    /// Interaction Event that calls a standard method from an IInteractable interface on this gameObject
    /// </summary>
    [Serializable]
    internal class Interaction_Event_Interface : Interaction_Event
    {
        public override IEnumerator InteractionEventRoutine(Interactable interactable)
        {
            if (interactable.TryGetComponent(out IInteractable i))
            {
                i.OnInteract(interactable);
            }
            else
            {
                IbrahDebug.LogWarning("No interface of type IInteractable found");
            }

            yield return null;
        }
    }
}