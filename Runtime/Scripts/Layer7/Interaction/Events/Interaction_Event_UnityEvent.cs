#region

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace IbrahKit.Interaction
{
    [Serializable]
    public class Interaction_Event_UnityEvent : Interaction_Event_Extension
    {
        [SerializeField] private UnityEvent unityEvent;

        public override IEnumerator InteractionEventRoutine(Interactable interactable)
        {
            unityEvent.Invoke();
            yield return null;
        }
    }
}
