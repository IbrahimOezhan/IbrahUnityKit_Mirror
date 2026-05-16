#region

using System;
using System.Collections;

#endregion

namespace IbrahKit.Interaction
{
    /// <summary>
    /// Base class for interaction events providing an abstract method to execute the interaction
    /// </summary>
    [Serializable]
    public abstract class Interaction_Event
    {
        public abstract IEnumerator InteractionEventRoutine(Interactable interactable);
    }
}