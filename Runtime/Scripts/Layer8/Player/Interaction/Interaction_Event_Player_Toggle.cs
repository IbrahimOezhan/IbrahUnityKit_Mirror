#region

using System;
using System.Collections;
using IbrahKit.Interaction;
using UnityEngine;

#endregion

namespace IbrahKit
{
    [Serializable]
    public class Interaction_Event_Player_Toggle : Interaction_Event_Extension
    {
        [SerializeField] private bool playerControllerActive;

        public override IEnumerator InteractionEventRoutine(Interactable interactable)
        {
            //Player_Movement.main.active = playerControllerActive;
            yield return null;
        }
    }
}