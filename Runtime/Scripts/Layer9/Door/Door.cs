#region

using System;
using System.Collections;
using UnityEngine;

#endregion

namespace IbrahKit.Interaction
{
    public partial class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] Animator anim;
        [SerializeField] private DoorState state;
        [SerializeField] private Interactable[] interactables;
        [SerializeField] private bool preventOpening;

        private void Start()
        {
            switch (state)
            {
                case DoorState.Closed:
                    anim.Play("Closed", 0, 1);
                    break;
                default:
                    anim.Play("Open", 0, 1);
                    break;
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < interactables.Length; i++)
            {
                interactables[i].enabled = !(preventOpening && state == DoorState.Closed);
            }
        }

        public void OnInteract(Interactable i)
        {
            string _clipName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (_clipName == "Closed" || _clipName == "Open")
            {
                switch (state)
                {
                    case DoorState.Open:
                        anim.Play("Closing");
                        state = DoorState.Closed;
                        break;
                    default:
                        anim.Play("Opening");
                        state = DoorState.Open;
                        break;
                }
            }
        }

        public IEnumerator OnInteractRoutine(Interactable _interactable)
        {
            throw new NotImplementedException();
        }

        public void SetPreventOpen(bool value)
        {
            preventOpening = value;
        }
    }
}