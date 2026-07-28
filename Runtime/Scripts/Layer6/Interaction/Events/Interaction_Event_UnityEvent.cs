#region

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace IbrahKit.Interaction
{
    /// <summary>
    ///     Interaction Event that executes a unity event
    /// </summary>
    [Serializable]
    internal class Interaction_Event_UnityEvent : Interaction_Event
    {
        [SerializeField] private UnityEvent unityEvent;

        public override IEnumerator InteractionEventRoutine(Interactable interactable)
        {
            unityEvent.Invoke();
            yield return null;
        }
    }
}