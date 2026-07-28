#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit
{
    public class Player_Movement_Top : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private Vector2 input;
        private Input2D input2D;
        private Rigidbody2D rigidbody2d;

        private void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            input2D = new();
        }

        private void Update()
        {
            Movement();
        }

        private void OnEnable()
        {
            input2D.Enable();
            input2D.PlayerTopDown.Move.started += Move;
            input2D.PlayerTopDown.Move.performed += Move;
            input2D.PlayerTopDown.Move.canceled += Move;
        }

        private void OnDisable()
        {
            input2D.PlayerTopDown.Move.started -= Move;
            input2D.PlayerTopDown.Move.performed -= Move;
            input2D.PlayerTopDown.Move.canceled -= Move;
            input2D.Dispose();
        }

        private void Move(InputAction.CallbackContext _context)
        {
            input = _context.ReadValue<Vector2>();
        }

        void Movement()
        {
            if (rigidbody2d.linearVelocity.x > 0f) GetComponent<SpriteRenderer>().flipX = false;
            else if (rigidbody2d.linearVelocity.x < 0f) GetComponent<SpriteRenderer>().flipX = true;
            rigidbody2d.linearVelocity = input * moveSpeed;
        }
    }
}