#region

using IbrahKit.Collision;
using UnityEngine;

#endregion

namespace IbrahKit.ThreeDPlayer
{
    public class Player_Moving_Platform : MonoBehaviour, ITrigger
    {
        private bool sync;

        private void Update()
        {
            if (sync) Physics.SyncTransforms();
        }

        public void TriggerEnter(Collider _player, Collider _hit)
        {
            sync = true;
            //Player_Movement.main.transform.parent = transform;
        }

        public void TriggerExit(Collider _player, Collider _hit)
        {
            //Player_Movement.main.transform.parent = null;
            sync = false;
        }

        public void TriggerStay(Collider _player, Collider _hit)
        {
        }
    }
}