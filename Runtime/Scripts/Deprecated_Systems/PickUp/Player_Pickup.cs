#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.ThreeDPlayer;
using IbrahKit.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using Time = UnityEngine.Time;

#endregion

namespace IbrahKit.Interaction
{
    public class Player_Pickup : MonoBehaviour
    {
        public static Player_Pickup Instance;

        [SerializeField] private bool showLine;
        [SerializeField] private int pointsCount = 100;
        [SerializeField] private float timeStep = 0.1f;
        [SerializeField] private float forceMultiplier;
        [SerializeField] private float strenghtTimeMultiplier;

        [SerializeField] private Vector2 strenghtClamp;
        [SerializeField] private LayerMask layerMask;

        [SerializeField] private Pickup_State state;

        [SerializeField] private Transform hand;
        [SerializeField] private Animator handAnim;
        [SerializeField] private LineRenderer trajectoryLine;
        [SerializeField] private Interactable_Pickup currentObject;
        [SerializeField] private CharacterController cc;

        private Transform cameraTr;

        private Vector3 force;
        private bool impulse;
        private Player3D_Input input;
        private float posLerp;
        private Vector3 startPos;
        private Quaternion startRot;

        private float strenght;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else
            {
                Instance = this;

                input = new();

                cameraTr = Camera.main.transform;
            }
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case Pickup_State.NONE:

                    if (impulse)
                    {
                        input.Player.Crouch.canceled -= Drop;
                        input.Player.Crouch.performed -= StartDrop;
                        impulse = false;
                    }

                    break;

                case Pickup_State.PICKINGUP:

                    if (impulse)
                    {
                        impulse = false;
                    }

                    (Vector3 posGoal, Quaternion rotGoal) = currentObject.GetPosData(hand);

                    posLerp += Time.deltaTime * 4;

                    currentObject.transform.SetPositionAndRotation(
                        Vector3.Lerp(startPos, posGoal, Mathf.Pow(posLerp, 2)),
                        Quaternion.Lerp(startRot, rotGoal, Mathf.Pow(posLerp, 2)));

                    if (posLerp >= 1)
                    {
                        ChangeState(Pickup_State.PICKEDUP);
                    }

                    break;
                case Pickup_State.PICKEDUP:

                    if (impulse)
                    {
                        input.Player.Crouch.canceled -= Drop;
                        input.Player.Crouch.performed += StartDrop;
                        impulse = false;
                    }

                    handAnim.speed = Math_Utilities.Map(cc.velocity.magnitude, 0, 6, .5f, 2.5f);

                    break;

                case Pickup_State.DROPPING:

                    if (impulse)
                    {
                        input.Player.Crouch.performed -= StartDrop;
                        input.Player.Crouch.canceled += Drop;
                        impulse = false;
                    }

                    strenght = Mathf.Clamp(strenght + Time.deltaTime * strenghtTimeMultiplier, strenghtClamp.x,
                        strenghtClamp.y);

                    force = cameraTr.transform.forward * (strenght * forceMultiplier);

                    break;
            }
        }

        private void OnEnable()
        {
            input.Enable();

            Application.onBeforeRender += UpdateTrajectory;
        }

        private void OnDisable()
        {
            if (input != null)
            {
                input.Disable();

                Application.onBeforeRender -= UpdateTrajectory;
            }
        }

        public event Action<Interactable_Pickup> OnPickedUp;
        public event Action<Interactable_Pickup> OnDropped;

        public void ChangeState(Pickup_State newState)
        {
            state = newState;
            impulse = true;
        }

        private void UpdateTrajectory()
        {
            if (showLine && GetPickupState() == Pickup_State.DROPPING)
            {
                Vector3 initialPos = currentObject.transform.position;
                trajectoryLine.positionCount = pointsCount;

                for (int i = 0; i < pointsCount; i++)
                {
                    float t = i * timeStep;

                    Vector3 point = initialPos + force * t + 0.5f * (t * t) * Physics.gravity;

                    trajectoryLine.SetPosition(i, point);

                    Vector3 halfExtens = new(0.05f, 0.05f, 0.05f);

                    List<Collider> colls = Physics
                        .OverlapBox(point, halfExtens, Quaternion.identity, LayerMask.NameToLayer("Hand")).ToList();

                    if (colls.Count > 0 && colls.FindAll(x => x.isTrigger).Count != colls.Count)
                    {
                        trajectoryLine.positionCount = i;
                        return;
                    }
                }
            }
            else trajectoryLine.positionCount = 0;
        }

        public void PickUpObject(Interactable_Pickup pickup, float lerpProgress = 0)
        {
            if (GetPickupState() != Pickup_State.NONE) return;

            currentObject = pickup;

            currentObject.transform.parent = hand;

            posLerp = lerpProgress;

            startPos = currentObject.transform.position;

            startRot = currentObject.transform.rotation;

            OnPickedUp?.Invoke(currentObject);
        }

        public void StartDrop(InputAction.CallbackContext context)
        {
            if (currentObject.PreventDrop() || !AllowDrop()) return;

            ChangeState(Pickup_State.DROPPING);
        }

        public void Drop(InputAction.CallbackContext context)
        {
            if (AllowDrop())
            {
                currentObject.Drop();

                Rigidbody _rb = currentObject.GetComponent<Rigidbody>();

                _rb.AddForce(force, ForceMode.Impulse);

                Drop_Generic();

                ChangeState(Pickup_State.PICKEDUP);
            }
            else
            {
                ChangeState(Pickup_State.NONE);
            }

            strenght = 0;
        }

        public void Drop_Pos(Transform tr)
        {
            if (currentObject != null)
            {
                currentObject.DropStatic();

                currentObject.transform.SetPositionAndRotation(tr.position, tr.rotation);

                currentObject.transform.parent = tr;

                Drop_Generic();
            }
        }

        private void Drop_Generic()
        {
            OnDropped?.Invoke(currentObject);

            currentObject = null;
        }

        public void DestroyObject()
        {
            Destroy(currentObject.gameObject);

            currentObject = null;
        }

        public Interactable_Pickup GetCurrHoldingOb()
        {
            return currentObject;
        }

        public Pickup_State GetPickupState()
        {
            return state;
        }

        public bool TryGetHand(out Interactable_Pickup result)
        {
            result = currentObject;

            return currentObject != null;
        }

        private bool AllowDrop()
        {
            Vector3 origin = transform.position + new Vector3(0, 1, 0);
            Vector3 direction = currentObject.transform.position - origin;
            Physics.Raycast(origin, direction, out RaycastHit info, 3, layerMask);
            return info.transform == currentObject.transform && !currentObject.IsColliding();
        }

        public bool IsHandFull()
        {
            return currentObject != null;
        }
    }
}