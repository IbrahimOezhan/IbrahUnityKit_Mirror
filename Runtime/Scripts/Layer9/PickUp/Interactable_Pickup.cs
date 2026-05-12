#region

using System;
using System.Collections;
using System.Collections.Generic;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Interaction
{
    public class Interactable_Pickup : Interactable, IInteractable
    {
        private int handLayer;
        private Rigidbody rb;
        private Collider coll;
        private List<Transform> children = new();

        private int pickedUpCounter;

        [SerializeField] private bool isColliding;
        [SerializeField] private bool preventDrop;
        [SerializeField] private Vector3 rotOffset;
        [SerializeField] private Vector3 posOffset;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();
            children = transform.BetterGetComponentsInChildren<Transform>();
            handLayer = LayerMask.NameToLayer("Hand");
        }

        private void FixedUpdate()
        {
#if UNITY_6000_0_OR_NEWER
            Vector3 velocity = rb.linearVelocity;
#else
            Vector3 velocity = rb.velocity;
#endif

            if (pickedUpCounter > 0 && velocity.magnitude > 20)
            {
                Pickup(.99f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.isTrigger) isColliding = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.isTrigger) isColliding = false;
        }

        public override bool CanInteract()
        {
            return !Player_Pickup.Instance.IsHandFull();
        }

        public void OnInteract(Interactable _interactable)
        {
            Pickup();
        }

        public void Pickup(float lerpProgress = 0)
        {
            pickedUpCounter++;

            coll.isTrigger = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            gameObject.layer = handLayer;
            for (int i = 0; i < children.Count; i++) children[i].gameObject.layer = handLayer;

            Player_Pickup.Instance.PickUpObject(this, lerpProgress);

            OnPickup();
        }

        public virtual void OnPickup()
        {

        }

        public void Drop()
        {
            DropGeneric();
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        public void DropStatic()
        {
            DropGeneric();
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        private void DropGeneric()
        {
            coll.isTrigger = false;
            transform.parent = null;
            gameObject.layer = 0;
            for (int i = 0; i < children.Count; i++) children[i].gameObject.layer = 0;
        }

        public IEnumerator OnInteractRoutine(Interactable _interactable)
        {
            throw new NotImplementedException();
        }

        public bool IsColliding()
        {
            return isColliding;
        }

        public bool PreventDrop()
        {
            return preventDrop;
        }

        public (Vector3, Quaternion) GetPosData(Transform hand)
        {
            Vector3 posGoal = hand.TransformPoint(hand.transform.localPosition + posOffset);
            Quaternion rotGoal = Quaternion.Euler((hand.transform.eulerAngles + rotOffset));

            return (posGoal, rotGoal);
        }
    }
}