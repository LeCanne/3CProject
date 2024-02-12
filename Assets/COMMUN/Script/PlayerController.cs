using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.XR;
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
        CapsuleCollider capCollider;
        public PhysicMaterial pm;

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
        public PiedestalController piedestal;
        public InventoryController theInventory;

        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask whatIsGround;
        bool grounded;

        [Header("Slope Handling")]
        public float maxSlopeAngle;
        private RaycastHit slopeHit;
        private bool exitingSlope;

        Vector3 moveDirection;

        private void Awake()
        {
            inputActions = new();
            move = inputActions.Gameplay.Move;
            jump = inputActions.Gameplay.Jump;
            rb = GetComponent<Rigidbody>();
            capCollider = GetComponent<CapsuleCollider>();

            inputActions = new Player();
            //_input = GetComponent<PlayerInput>();


        }

        void Start()
        {
            
        }

        private void Update()
        {
            // Boutons pour ouvrir/fermer l'inventaire
            if (Input.GetButtonDown("Fire1") && !PiedestalController.canPut)
            {
                CameraController.noUseCamera = true;
                panelInventaire.SetActive(true);
                Time.timeScale = 0f;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                CameraController.noUseCamera = false;
                panelInventaire.SetActive(false);
                Time.timeScale = 1f;
            }

            // On détecte le sol pour modifier la friction du physic material
            Ray ray = new Ray(this.transform.position + Vector3.up * -0.20f, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 1f))
            {
                if (OnSlope() && !exitingSlope)
                {

                }
                else
                {
                    pm.staticFriction = 3f;
                    pm.dynamicFriction = 3f;
                }
                  
            }
            else
            {
                pm.staticFriction = 0.6f;
                pm.dynamicFriction = 0.05f;
            }

            if (Input.GetButtonDown("Fire3") && ItemController.canTake)
            {
                item.TakeObject();
            }

            if (Input.GetButtonDown("Fire3") && PiedestalController.canPut)
            {
                CameraController.noUseCamera = true;
                panelInventaire.SetActive(true);
                Time.timeScale = 0f;
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
                rb.velocity += Vector3.down * (Physics.gravity.y * -2f) * Time.fixedDeltaTime;
            }

            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0;
            if (horizontalVelocity.sqrMagnitude > MaxSpeed * MaxSpeed)
            {
                rb.velocity = horizontalVelocity.normalized * MaxSpeed + Vector3.up * rb.velocity.y;
            }

            // Quand on est sur la pente
            if (OnSlope() && !exitingSlope)
            {
                Debug.Log("OnSlope");
                rb.AddForce(GetSlopeMoveDirection() * MaxSpeed * 20f, ForceMode.Force);
                rb.drag = 2f;

                pm.staticFriction = 1f;
                pm.dynamicFriction = 1f;

                


                


            }
            else
            {
                
                rb.drag = 0f;
            }

            // Rester sur la pente sans glisser
            rb.useGravity = !OnSlope();
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

        // Méthode pour la pente
        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(forceDirection, slopeHit.normal).normalized;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Détecter les objets récupérables
            if (other.CompareTag("Item"))
            {
                item = other.gameObject.GetComponent<ItemController>();
            }
        }

        private void OnTriggerExit(Collider other) 
        {
            if (other.CompareTag("Item"))
            {
                item = null;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(new Ray(this.transform.position + Vector3.up * -0.10f, Vector3.down));
        }
    }
}
// Melvin LECANNE
// Charles DUVERGER