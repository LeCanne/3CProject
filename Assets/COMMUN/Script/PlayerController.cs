using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3CFeel.Controller
{
    public class PlayerController : MonoBehaviour
    {
        //PlayerInput _input;
        Player inputActions;

        InputAction move;
        InputAction jump;

        Rigidbody rb;

        private void Awake()
        {
            inputActions = new();
            move = inputActions.Gameplay.Move;
            jump = inputActions.Gameplay.Jump;
            rb = GetComponent<Rigidbody>();

            //_input = GetComponent<PlayerInput>();
        }

        void Start()
        {
            
        }

        
        void Update()
        {
            OnMove();
        }

        private void OnEnable()
        {
            move.Enable();
        }

        private void OnDisable()
        {
            move.Disable();
        }

        public void OnMove()
        {
            rb.velocity = new(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
        }

        public void OnJump()
        {
            rb.velocity = new(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
        }
    }
}

