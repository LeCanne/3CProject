using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        public GameObject spine;

        public GameObject panelInventaire;
        public GameObject content;
        public GameObject iconeInventaire;

        [Header("PlayerSettings")]
        public float MaxSpeed;
        private float OGspeed;
        public float rotationSpeed;
        private Vector3 forceDirection = Vector3.zero;
        private float movementForce = 1f;
        public float timerMove;
        public float jumpForce = 5f;
        public GameObject skin;

        [Header("AttachedElements")]
        public Camera Camera;

        [Header("Script")]
        public ItemController item;
        public PiedestalController piedestal;
        public InventoryController theInventory;
        public CameraController camControl;
        public AudioManager theAudio;

        [Header("Ground Check")]
        public float PlayerHeight;
        public LayerMask whatIsGround;
        bool grounded;

        [Header("Slope Handling")]
        public float MaxSlopeAngle;
        private RaycastHit slopeHit;
        private bool exitingSlope;
        private bool hasExited;
    

        Vector3 moveDirection;

        public Animator anim;
        public float timerIdle, timerFall;
        private bool checkIdle, isFall;

        private void Awake()
        {
            theAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();

            inputActions = new();
            move = inputActions.Gameplay.Move;
           // jump = inputActions.Gameplay.Jump;
            rb = GetComponent<Rigidbody>();
            capCollider = GetComponent<CapsuleCollider>();
            
            inputActions = new Player();
            //_input = GetComponent<PlayerInput>();
            OGspeed = MaxSpeed;

        }

        void Start()
        {
            timerIdle = 9;
        }

        private void Update()
        {
            
            skin.transform.localPosition = new Vector3(0, 0.6f, 0);
           
            Animations();


            // Boutons pour ouvrir/fermer l'inventaire
            if (Input.GetButtonDown("Fire1") && !PiedestalController.canPut1 && !CameraController.noUseCamera || Input.GetButtonDown("Fire1") && !PiedestalController.canPut2 && !CameraController.noUseCamera)
            { 
                theAudio.OpenBag();
                CameraController.noUseCamera = true;
                InventoryController.noUseInventory = true;
                panelInventaire.SetActive(true);
                iconeInventaire.SetActive(false);

                if (content.GetComponentInChildren<Button>())
                    EventSystem.current.SetSelectedGameObject(content.transform.GetChild(0).gameObject);
            }
            
            if (Input.GetButtonDown("Fire2") && InventoryController.noUseInventory)
            {
                InventoryController.noUseInventory = false;
                CameraController.noUseCamera = false;
                panelInventaire.SetActive(false);
                theInventory.buttonUse.SetActive(false);
                iconeInventaire.SetActive(true);
                Time.timeScale = 1f;

                if (!PiedestalController.canPut1 && !PiedestalController.clear1 || !PiedestalController.canPut2 && !PiedestalController.clear2)
                {
                    piedestal.StartCoroutine(piedestal.PutobjectOn());
                }
            }

            // On détecte le sol pour modifier la friction du physic material
            Ray ray = new Ray(this.transform.position + Vector3.up * -0.20f, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 1f))
            {
                if (OnSlope() == false)
                {

                    pm.staticFriction = 0f;
                    pm.dynamicFriction = 0f;



                }
                
                  
            }
            else
            {
                pm.staticFriction = 0.6f;
                pm.dynamicFriction = 0.05f;
            }

            // On récupère un objet
            if (Input.GetButtonDown("Fire3") && ItemController.canTake)
            {
                item.TakeObject();

                // On veut savoir si l'objet est sur un piédestaux ou non
                if (piedestal != null) 
                { 
                    if (piedestal.category == PiedestalController.CATEGORY.PIEDESTAL1)
                    {
                        PiedestalController.havePut1 = false;
                        Debug.Log(PiedestalController.havePut1);

                        //SpriteRenderer srDroite = piedestal.oeilDroite.GetComponent<SpriteRenderer>();
                        //srDroite.material = piedestal.matInit;
                    }

                    if (piedestal.category == PiedestalController.CATEGORY.PIEDESTAL2)
                    {
                        PiedestalController.havePut2 = false;
                        Debug.Log(PiedestalController.havePut2);

                        //SpriteRenderer srGauche = piedestal.oeilGauche.GetComponent<SpriteRenderer>();
                        //srGauche.material = piedestal.matInit;
                    }
                }
            }

            // On ouvre l'inventaire pour déposer un objet
            if (Input.GetButtonDown("Fire3") && PiedestalController.canPut1 && !InventoryController.noUseInventory || Input.GetButtonDown("Fire3") && PiedestalController.canPut2 && !InventoryController.noUseInventory)
            {
                theAudio.OpenBag();
                piedestal.StartCoroutine(piedestal.PutobjectOf());
                CameraController.noUseCamera = true;
                panelInventaire.SetActive(true);
                iconeInventaire.SetActive(false);

                InventoryController.haveButton = true;
                InventoryController.noUseInventory = true;

                if (content.GetComponentInChildren<Button>())
                    EventSystem.current.SetSelectedGameObject(content.transform.GetChild(0).gameObject);
            }
        }

        void FixedUpdate()
        {
            OnMove();
        }

        private void OnEnable()
        {
            move.Enable();

            //inputActions.Gameplay.Jump.started += DoJump;
            inputActions.Gameplay.Enable();
        }

        private void OnDisable()
        {
            move.Disable();

            inputActions.Gameplay.Jump.started -= DoJump;
            inputActions.Gameplay.Disable();
        }

        public void Animations()
        {
            anim.SetFloat("VelocityX", Mathf.Abs(move.ReadValue<Vector2>().magnitude));
            anim.SetFloat("VelocityZ", rb.velocity.z);
            anim.SetFloat("TimeIdle", timerIdle);
            anim.SetFloat("TimeFall", timerFall);
            anim.SetBool("IsFalling", isFall);

            if (rb.velocity.x != 0 ||  rb.velocity.z != 0) 
            {
                checkIdle = false;
                timerIdle = 9;
               
            }

            if (rb.velocity.x == 0f && rb.velocity.z == 0f) 
            {
                timerIdle -= Time.deltaTime;
                
                if (timerIdle <= 0)
                {
                    if(checkIdle == false)
                    {
                        anim.SetTrigger("IsIdle");
                        checkIdle = true;
                       
                    }
                   
                    
                }

                
              
            }

            if (!IsGrounded())
            {
                timerFall += Time.deltaTime;
            }
            else
            {
                if (timerFall >= 0.5f)
                    theAudio.Falling();

                timerFall = 0;
            }
        }

        public void OnMove()
        {


           
                forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(Camera) * movementForce;
               
            
           

            if(Mathf.Abs(move.ReadValue<Vector2>().y) > 0.2f)
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(Camera) * movementForce;

            rb.AddForce(forceDirection, ForceMode.Impulse);
            if (forceDirection != Vector3.zero || camControl.cameraState == CameraController.CAMERASTATES.CLOSE)
            {
                if (camControl.cameraState != CameraController.CAMERASTATES.CLOSE)
                {
                    Vector3 direction = forceDirection;
                    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    skin.transform.rotation = Quaternion.Slerp(skin.transform.rotation, toRotation, Time.fixedDeltaTime / 0.06f);
                    direction.y = 0;
                   

                }
                else
                {
                   
                    
                        skin.transform.localEulerAngles = new Vector3(0, camControl.gameObject.transform.eulerAngles.y, 0);
                  
                    
                }
               
                 
               
               
               
                
            }
            if (OnSlope() && !exitingSlope)
            {
                if (forceDirection != Vector3.zero)
                {
                    
                    rb.AddForce(GetSlopeMoveDirection(), ForceMode.Force);
                }



                if (rb.velocity.y > 0)
                {
                    rb.velocity = Vector3.ClampMagnitude(rb.velocity, 10);

                }
                if (rb.velocity.y < 0)
                {
                    float yVel = rb.velocity.y;
                    yVel = Mathf.Clamp(yVel, -10f, 0f);

                    rb.velocity = new Vector3(rb.velocity.x, yVel, rb.velocity.z);
                }





                hasExited = true;

                if (rb.velocity.y > 0 && IsGrounded() == false)
                {
                    rb.AddForce(Vector3.down * 1f, ForceMode.Force);
                    Debug.Log("GoDown");
                }
            }
            else
            {

                if (hasExited == true)
                {
                    rb.AddForce(Vector3.down * (3.9f - GetSlopeMoveDirection().z / 2), ForceMode.Impulse);
                    hasExited = false;
                    Debug.Log("GoDownAgain");
                }

            }
            forceDirection = Vector3.zero;
            

            if (move.ReadValue<Vector2>().x == 0 && move.ReadValue<Vector2>().y == 0 && IsGrounded() == true )
            {
                if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.z) > 0)
                {

                    
                    rb.drag = 8;
                }

                timerMove = 0;

            }
            else
            {
                Debug.Log("yes");
                timerMove += Time.deltaTime;
                if (camControl.cameraState != CameraController.CAMERASTATES.CLOSE)
                {
                     
                 if (rb.velocity.magnitude > 5f)
                 {
                    Debug.Log(rb.velocity.magnitude);
                    rb.drag = 1.2f;
                 }
                 else
                 {
                     rb.drag = 5;
                 }
                }
               


               
               
            }

            if (rb.velocity.y < 0f && !OnSlope())
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
          

            // Rester sur la pente sans glisser
            rb.useGravity = !OnSlope();
        }

        public void DoJump(InputAction.CallbackContext obj)
        {
            Debug.Log("Detect");
            if (IsGrounded() && !CameraController.noUseCamera)
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
            Ray ray = new Ray(this.transform.position + Vector3.up * -0.20f, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 1.3f))
            {
                isFall = false;
                return true;
            }
            else
            {
                isFall = true;
                return false;
            }
        }

        // Méthode pour la pente
        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, PlayerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < MaxSlopeAngle && angle != 0;
            }

            return false;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            
            return Vector3.ProjectOnPlane(forceDirection, slopeHit.normal);
            
        }

        private void OnTriggerEnter(Collider other)
        {
            // Détecter les objets récupérables
            if (other.CompareTag("Item"))
            {
                item = other.gameObject.GetComponent<ItemController>();
            }

            if (other.CompareTag("Piedestal"))
            {
                piedestal = other.gameObject.GetComponent<PiedestalController>();
            }

            //Detecter les changements de Camera

            if (other.CompareTag("CloseCamera"))
            {
                camControl.cameraState = CameraController.CAMERASTATES.CLOSE;
                MaxSpeed = 3;
                anim.SetBool("isWalk", true);
            }

            if (other.CompareTag("DefaultCamera"))
            {
                anim.SetBool("isWalk", false);
                camControl.cameraState = CameraController.CAMERASTATES.DEFAULT;
                MaxSpeed = OGspeed;
            }

            if (other.CompareTag("Slow"))
            {
              
                MaxSpeed = 3;
                anim.SetBool("isWalk", true);
            }

            if (other.CompareTag("Fast"))
            {

                MaxSpeed = OGspeed;
                anim.SetBool("isWalk", false);
            }
        }

        private void OnTriggerExit(Collider other) 
        {
            if (other.CompareTag("Item"))
            {
                item = null;
            }

            if (other.CompareTag("Piedestal"))
            {
                piedestal = null;
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