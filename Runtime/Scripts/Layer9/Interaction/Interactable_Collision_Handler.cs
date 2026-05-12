#region

using IbrahKit.Collision;
using UnityEngine;

#endregion

namespace IbrahKit.Interaction
{
    public class Interactable_Collision_Handler : Collision_Handler<Interactable_Collider, ICollision>
    {
        [SerializeField] private ThreeD_Player_Interaction_Manager manager;

        protected override void TriggerEnter(Interactable_Collider trigger)
        {
            manager.SetCollInteratable(trigger);
        }

        protected override void TriggerExit(Interactable_Collider trigger)
        {
            manager.SetCollInteratable(null);
        }
    }

}