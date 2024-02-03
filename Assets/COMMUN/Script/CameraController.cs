using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] float distance = 5;

    [SerializeField] float minVerticalAngle = -45f;
    [SerializeField] float maxVerticalAngle = 45f;

    float rotationX;
    float rotationY;
    public float offsetY;

    [Header("Settings")]
    public float rotationspeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse Y") * rotationspeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        rotationY += Input.GetAxis("Mouse X") * rotationspeed;

        var targetRotation = Quaternion.Euler(-rotationX, rotationY, 0);

        transform.position = Vector3.Lerp(transform.position,followTarget.position - targetRotation * new Vector3(0, offsetY, distance), Time.deltaTime / 0.02f);
        transform.rotation = targetRotation;
    }
}
