using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    

    public float cameraSmoothingFactor = 1;
    public float cameraSmoothingFactorY;

    public float minVerticalAngle = -45f;
    public float maxVerticalAngle = 45f;
    
    public static bool noUseCamera;
    public float timerCameraUse;
    public bool inclose;
    public Transform t_camera;
    private Vector3 camera_offset;
    private Vector3 OriginalPos;
    private Quaternion camRotation;
    private RaycastHit hit;
    public GameObject closePos;
    public GameObject rotatorSkin;
    public _3CFeel.Controller.PlayerController playerCon;

    public float speedjoystick;
    public enum CAMERASTATES
    {
        DEFAULT,
        CLOSE

    }
    public CAMERASTATES cameraState;
    public Material matShader;

    // Start is called before the first frame update
    void Start()
    {
        camRotation = transform.localRotation;
        camera_offset = t_camera.localPosition;
        OriginalPos = transform.localPosition;
        cameraSmoothingFactorY = cameraSmoothingFactor;

        noUseCamera = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetAxis("Joystick X") != 0 || Input.GetAxis("Joystick Y") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            timerCameraUse = 0;
        }
        else
        {
            timerCameraUse += Time.deltaTime;
        }
        if (Physics.Linecast(transform.position, (transform.position + transform.localRotation * camera_offset) - t_camera.transform.forward, out hit) && inclose == false)
        {
            t_camera.localPosition = new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point + t_camera.transform.forward));
        }
        else
        {
            t_camera.localPosition = Vector3.Lerp(t_camera.localPosition, camera_offset, Time.deltaTime);
        }
        if (noUseCamera == false)
        {
            camRotation.x += Input.GetAxis("Joystick Y") * cameraSmoothingFactorY * -speedjoystick * Time.deltaTime;
            camRotation.y += Input.GetAxis("Joystick X") * cameraSmoothingFactor * speedjoystick * Time.deltaTime;

            camRotation.x += Input.GetAxis("Mouse Y") * cameraSmoothingFactorY * (-1);
            camRotation.y += Input.GetAxis("Mouse X") * cameraSmoothingFactor;

            camRotation.x = Mathf.Clamp(camRotation.x, minVerticalAngle, maxVerticalAngle);
            
            
            
            transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);

           
        }
        else
        {
            t_camera.rotation = transform.localRotation;
        }

        if(cameraState == CAMERASTATES.DEFAULT)
        {
            inclose = false;
            noUseCamera = false;
            camera_offset = new Vector3(0, 0, -5.2f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, OriginalPos, Time.deltaTime);
       
            matShader.SetFloat("_SeeThroughDistance", 1.8f);

            //EquationAxeX
            if (camRotation.x != 0)
            {
                cameraSmoothingFactor = 2 / (Mathf.Abs(camRotation.x) / 10 + 1);
            }
            
            if(camRotation.x != 0 && timerCameraUse > 1 && playerCon.timerMove > 1)
            {
                camRotation.x = Mathf.LerpAngle(camRotation.x, 0 , 1 * Time.deltaTime) ;
            }
        }
        if (cameraState == CAMERASTATES.CLOSE)
        {
          
            cameraSmoothingFactor = 1.3f;
            float distance = Vector3.Distance(transform.position, closePos.transform.position);
            if(distance > 0.5f)
            {
               
                transform.position = Vector3.Lerp(transform.position, closePos.transform.position, Time.deltaTime / 0.1f);

            }
            else
            {
               
                transform.position = closePos.transform.position;
            }
         
            camera_offset = new Vector3(0f, 0, -0.3f);
            matShader.SetFloat("_SeeThroughDistance",-0.5f);
        }
    }
}
