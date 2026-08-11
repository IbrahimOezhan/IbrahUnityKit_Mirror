#region

using Sirenix.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Collision
{
    /// <summary>
    ///     Script that sits on the Player and listens for collisions
    /// </summary>
    public abstract class Collision_Handler<TTrigger, TCollision> : MonoBehaviour
        where TTrigger : ITrigger where TCollision : ICollision
    {
        [SerializeField] private Collider playerCollider;

        private void Awake()
        {
            playerCollider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            TCollision[] collisions = collision.gameObject.GetComponents<TCollision>();
            collisions.ForEach(CollisionEnter);
        }

        private void OnCollisionExit(UnityEngine.Collision collision)
        {
            TCollision[] collisions = collision.gameObject.GetComponents<TCollision>();
            collisions.ForEach(CollisionExit);
        }

        private void OnCollisionStay(UnityEngine.Collision collision)
        {
            TCollision[] collisions = collision.gameObject.GetComponents<TCollision>();
            collisions.ForEach(CollisionStay);
        }

        private void OnTriggerEnter(Collider cCollider)
        {
            TTrigger[] triggerCollisions = cCollider.GetComponents<TTrigger>();

            triggerCollisions.ForEach(TriggerEnter);
        }

        private void OnTriggerExit(Collider cCollider)
        {
            TTrigger[] triggerCollisions = cCollider.GetComponents<TTrigger>();
            triggerCollisions.ForEach(TriggerExit);
        }

        private void OnTriggerStay(Collider cCollider)
        {
            TTrigger[] triggerCollisions = cCollider.GetComponents<TTrigger>();
            triggerCollisions.ForEach(TriggerStay);
        }

        protected virtual void TriggerEnter(TTrigger trigger)
        {
        }

        protected virtual void TriggerStay(TTrigger trigger)
        {
        }

        protected virtual void TriggerExit(TTrigger trigger)
        {
        }

        protected virtual void CollisionEnter(TCollision collision)
        {
        }

        protected virtual void CollisionStay(TCollision collision)
        {
        }

        protected virtual void CollisionExit(TCollision collision)
        {
        }
    }
}