#region

using System.Collections.Generic;
using IbrahKit.Debugging;
using IbrahKit.InfoCollector;
using IbrahKit.ThreeDPlayer;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

#endregion

namespace IbrahKit.Interaction
{
    [DefaultExecutionOrder(-1)]
    public class ThreeD_Player_Interaction_Manager : Interaction_Manager, IDebug
    {
        private const string INTERACTABLE_TAG = "Interactable";

        private readonly float cooldownTimer = 0.1f;

        private bool canInteract;

        [FoldoutGroup("Debug"), ReadOnly, SerializeField]
        private Transform hitObject;

        private RaycastHit hit;

        private Player3D_Input input;

        private Transform cameraTr;

        [FoldoutGroup("Raycast"), SerializeField] private float distance;

        [FoldoutGroup("Raycast"), SerializeField] private LayerMask mask;

        [FoldoutGroup("Debug"), ReadOnly, SerializeField] private Interactable i;

        [FoldoutGroup("Debug"), ReadOnly, SerializeField] private Interactable collisionInteractable;

        [FoldoutGroup("Debug"), SerializeField] private bool preventInteraction;

        private static List<ThreeD_Player_Interaction_Manager> manager;
        
        protected void Awake()
        {
            cameraTr = Camera.main.transform;

            input = new();

            input.Enable();
        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneChanged;
            Lifecycle_Diagnostics_Manager.GetInstance().Add(this);
        }

        private void FixedUpdate()
        {
            StateMachine();
        }

        protected void OnDestroy()
        {
            if (input != null)
            {
                input.Disable();
            }

            SceneManager.sceneLoaded -= OnSceneChanged;
            
            if (Lifecycle_Diagnostics_Manager.TryGet(out Lifecycle_Diagnostics_Manager resultD))
            {
                resultD.Remove(this);
            }
        }

        private void StateMachine()
        {
            if (cameraTr == null)
            {
                IbrahDebug.LogWarning("Camera transform reference not set");

                return;
            }

            RunStateMachine();
        }

        protected override bool CanInteract(Interactable i)
        {
            return IsValidInteratable(i) && !preventInteraction;
        }

        protected override InputAction GetInteractInputAction()
        {
            return input.Player.Interact;
        }

        protected override Interactable FindInteractable()
        {
            //Prioritise Collision Interactable first
            Interactable i = collisionInteractable;

            //If detected return immediately
            if (i != null)
            {
                return i;
            }

            //Else look for interactable by raycast
            Vector3 origin = cameraTr.position;

            Vector3 dir = cameraTr.forward;

            if (Physics.Raycast(origin, dir, out hit, distance, mask)
                && hit.transform.TryGetComponent<Interactable>(out var interactable)
                && !hit.transform.CompareTag(INTERACTABLE_TAG))
            {
                i = interactable;
            }

            hitObject = hit.transform;

            Debug.DrawRay(origin, dir * distance, IsValidInteratable(i) ? Color.green : Color.red);

            return i;
        }
        
        public void SetCollInteratable(Interactable coll)
        {
            collisionInteractable = coll;
        }
        
        private bool IsValidInteratable(Interactable i)
        {
            return i != null && i.CanInteract() && i.enabled && i.gameObject.activeInHierarchy;
        }

        public Interactable GetCollInteratable() => collisionInteractable;

        private void OnSceneChanged(Scene scene, LoadSceneMode sceneLoad)
        {
            cameraTr = Camera.main.transform;
        }

        public string DebugContent()
        {
            if (hit.transform != null)
            {
                return "Looking at: " + hit.transform.gameObject.name;
            }

            return "Not looking at anything";
        }

        public int DebugOrder() => -100;


    }
}