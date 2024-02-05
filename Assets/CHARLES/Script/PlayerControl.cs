using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3CFeel.Controller
{
    public class PlayerControl : MonoBehaviour
    {
        //PlayerInput _input;
        Player inputActions;

        InputAction move;
        InputAction jump;

        Rigidbody rb;

        public GameObject panelInventaire;

        [Header("PlayerSettings")]
        public float MaxSpeed;
        public float rotationSpeed;
        private Vector3 forceDirection = Vector3.zero;
        private float movementForce = 1f;
        public float jumpForce = 5f;

        [Header("AttachedElements")]
        public Camera Camera;

        [Header("Script")]
        public ItemController item;
        public InventoryController theInventory;

        private void Awake()
        {
            inputActions = new();
            move = inputActions.Gameplay.Move;
            jump = inputActions.Gameplay.Jump;
            rb = GetComponent<Rigidbody>();

            inputActions = new Player();
            //_input = GetComponent<PlayerInput>();
        }

        void Start()
        {

        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                panelInventaire.SetActive(true);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                panelInventaire.SetActive(false);
            }
        }

        void FixedUpdate()
        {
            OnMove();
        }

        private void OnEnable()
        {
            move.Enable();

            inputActions.Gameplay.Jump.started += DoJump;
            inputActions.Gameplay.Enable();
        }

        private void OnDisable()
        {
            move.Disable();

            inputActions.Gameplay.Jump.started -= DoJump;
            inputActions.Gameplay.Disable();
        }

        public void OnMove()
        {


            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(Camera) * movementForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(Camera) * movementForce;

            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            if (rb.velocity.y < 0f)
            {
                rb.velocity += Vector3.down * (Physics.gravity.y * -2) * Time.fixedDeltaTime;
            }

            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0;
            if (horizontalVelocity.sqrMagnitude > MaxSpeed * MaxSpeed)
            {
                rb.velocity = horizontalVelocity.normalized * MaxSpeed + Vector3.up * rb.velocity.y;
            }
        }

        public void DoJump(InputAction.CallbackContext obj)
        {
            Debug.Log("Detect");
            if (IsGrounded())
            {
                forceDirection += Vector3.up * jumpForce;
                Debug.Log("ProcessJump");
            }
        }

        private Vector3 GetCameraForward(Camera playerCam)
        {
            Vector3 forward = playerCam.transform.forward;
            forward.y = 0;
            return forward.normalized;



        }

        private Vector3 GetCameraRight(Camera playerCam)
        {
            Vector3 forward = playerCam.transform.right;
            forward.y = 0;
            return forward.normalized;
        }

        private bool IsGrounded()
        {
            Ray ray = new Ray(this.transform.position + Vector3.up * -0.10f, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 1f))
            {
                Debug.Log("Detect");
                return true;
            }
            else
            {
                Debug.Log("Nope");
                return false;

            }


        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                theInventory.AddSlot(other.gameObject.GetComponent<ItemController>());
                Destroy(other.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(new Ray(this.transform.position + Vector3.up * -0.10f, Vector3.down));
        }
    }
}