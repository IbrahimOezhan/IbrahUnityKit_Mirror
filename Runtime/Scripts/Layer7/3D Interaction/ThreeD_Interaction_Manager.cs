#region

using System.Collections.Generic;
using IbrahKit.Debugging;
using IbrahKit.InfoCollector;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

#endregion

namespace IbrahKit.Interaction
{
    [DefaultExecutionOrder(-1)]
    public class ThreeD_Interaction_Manager : Interaction_Manager, IInfoCollector
    {
        private static List<ThreeD_Interaction_Manager> manager;

        [SerializeField] private string INTERACTABLE_TAG = "Interactable";
        
        [FoldoutGroup("Debug"), ReadOnly, SerializeField]
        private Transform hitObject;

        [FoldoutGroup("Raycast"), SerializeField]
        private float distance;

        [FoldoutGroup("Raycast"), SerializeField]
        private LayerMask mask;

        [FoldoutGroup("Debug"), ReadOnly, SerializeField]
        private Interactable interactable;

        [FoldoutGroup("Debug"), ReadOnly, SerializeField]
        private Interactable collisionInteractable;

        [FoldoutGroup("Debug"), SerializeField]
        private bool preventInteraction;

        [SerializeField] private Transform raycastOrigin;

        private bool canInteract;

        private RaycastHit hit;

        private Interaction_Input input;
        
        protected void Awake()
        {
            input = new();

            input.Enable();
        }

        private void Start()
        {
            Info_Collection_Manager.GetInstance().RegisterInfoCollector(this);
        }

        private void FixedUpdate()
        {
            StateMachine();
        }

        protected void OnDestroy()
        {
            input?.Disable();

            if (Info_Collection_Manager.TryGet(out Info_Collection_Manager resultD))
            {
                resultD.UnregisterInfoCollector(this);
            }
        }

        public string GetInformation()
        {
            if (hit.transform)
            {
                return "Looking at: " + hit.transform.gameObject.name;
            }

            return "Not looking at anything";
        }

        public int GetDebugOrder() => -100;

        private void StateMachine()
        {
            if (!raycastOrigin)
            {
                IbrahDebug.LogWarning("Camera transform reference not set");

                return;
            }

            RunStateMachine();
        }

        protected override bool CanInteract(Interactable i) => IsValidInteractable(i) && !preventInteraction;

        protected override InputAction GetInteractInputAction() => input.Map.Interact;

        protected override Interactable FindInteractable()
        {
            //Prioritize Collision Interactable first
            Interactable i = collisionInteractable;

            //If detected return immediately
            if (i) return i;

            //Else look for interactable by raycast
            Vector3 origin = raycastOrigin.position;

            Vector3 dir = raycastOrigin.forward;

            if (Physics.Raycast(origin, dir, out hit, distance, mask) && !hit.transform.CompareTag(INTERACTABLE_TAG))
                hit.transform.TryGetComponent(out i);

            hitObject = hit.transform;

            Debug.DrawRay(origin, dir * distance, IsValidInteractable(i) ? Color.green : Color.red);

            return i;
        }

        public void SetCollInteractable(Interactable coll)
        {
            collisionInteractable = coll;
        }

        private static bool IsValidInteractable(Interactable i)
        {
            return i && i.CanInteract() && i.enabled && i.gameObject.activeInHierarchy;
        }

        public Interactable GetCollInteractable() => collisionInteractable;
    }
}