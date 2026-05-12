#region

using System;
using System.Collections;

#endregion

namespace IbrahKit.Interaction
{
    [Serializable]
    public abstract class Interaction_Event_Extension
    {
        public abstract IEnumerator InteractionEventRoutine(Interactable interactable);
    }
}