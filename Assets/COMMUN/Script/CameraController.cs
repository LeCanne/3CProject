using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    

    public float cameraSmoothingFactor = 1;
    public float cameraSmoothingFactorY;

    public float minVerticalAngle = -45f;
    public float maxVerticalAngle = 45f;
    
    public static bool noUseCamera;
    public bool inclose;
    public Transform t_camera;
    private Vector3 camera_offset;
    private Vector3 OriginalPos;
    private Quaternion camRotation;
    private RaycastHit hit;
    public GameObject closePos;

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

    }

    // Update is called once per frame
    void Update()
    {

        if (noUseCamera == false)
        {
            camRotation.x += Input.GetAxis("Joystick Y") * cameraSmoothingFactorY * -speedjoystick * Time.deltaTime;
            camRotation.y += Input.GetAxis("Joystick X") * cameraSmoothingFactor * speedjoystick * Time.deltaTime;

            camRotation.x += Input.GetAxis("Mouse Y") * cameraSmoothingFactorY * (-1);
            camRotation.y += Input.GetAxis("Mouse X") * cameraSmoothingFactor;

            camRotation.x = Mathf.Clamp(camRotation.x, minVerticalAngle, maxVerticalAngle);
            
            
            
            transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);

            if (Physics.Linecast(transform.position, (transform.position + transform.localRotation * camera_offset) - t_camera.transform.forward, out hit) && inclose == false)
            {
                t_camera.localPosition = new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point + t_camera.transform.forward));
            }
            else
            {
                t_camera.localPosition = Vector3.Lerp(t_camera.localPosition, camera_offset, Time.deltaTime);
            }
        }

        if(cameraState == CAMERASTATES.DEFAULT)
        {
           camera_offset = new Vector3(0, 0, -5.2f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, OriginalPos, Time.deltaTime / 0.3f);
            inclose = false;
            matShader.SetFloat("_SeeThroughDistance", 1.8f);

            //EquationAxeX
            if (camRotation.x != 0)
            {
                cameraSmoothingFactor = 2 / (Mathf.Abs(camRotation.x) / 10 + 1);
            }
        }
        if (cameraState == CAMERASTATES.CLOSE)
        {
            cameraSmoothingFactor = 1.3f;
            transform.position = Vector3.Lerp(transform.position, closePos.transform.position, Time.deltaTime / 0.3f);
            camera_offset = new Vector3(0f, 0, -1f);
            matShader.SetFloat("_SeeThroughDistance",-0.5f);
        }
    }
}
