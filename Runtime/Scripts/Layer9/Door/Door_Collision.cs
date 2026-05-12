#region

using IbrahKit.Collision;
using UnityEngine;

#endregion

namespace IbrahKit.Interaction
{
    public class Door_Collision : MonoBehaviour, ITrigger
    {
        [SerializeField] private Door door;

        public void TriggerEnter(Collider _player, Collider _hit)
        {
            door.SetPreventOpen(true);
        }

        public void TriggerExit(Collider _player, Collider _hit)
        {
            door.SetPreventOpen(false);
        }

        public void TriggerStay(Collider _player, Collider _hit)
        {
            door.SetPreventOpen(true);
        }
    }
}