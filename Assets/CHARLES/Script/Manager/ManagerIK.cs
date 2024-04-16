using _3CFeel.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerIK : MonoBehaviour
{
    //Animator anim;
    //public bool ikActive = false;
    //public Transform objTarget;
    //public float lookWeight;

    public PlayerController thePlayer;

    public Transform headObject, targetObject, headForward;
    public float lookSpeed;
    bool isLooking;
    Quaternion lastRotation;
    float headResetTimer;
    public float MaxAngle, MinAngle;

    // Le pivot du perso
    GameObject objPivot;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();

        //// Le pivot du perso
        //objPivot = new GameObject("PersoPivot");
        //objPivot.transform.parent = transform.parent;
        //objPivot.transform.localPosition = new Vector3(0, 1.41f, 0);

        isLooking = false;

        thePlayer = GameObject.FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //objPivot.transform.LookAt(objTarget);
        //float pivotRotY = objPivot.transform.localRotation.y;

        //if (pivotRotY < 0.65f && pivotRotY > -0.65f)
        //{
        //    // Target tracking
        //    lookWeight = 1f;
        //}
        //else 
        //{
        //    // Target release
        //    lookWeight = 0f;
        //}
    }

    private void LateUpdate()
    {
        if (ItemController.canTake)
        {
            targetObject = thePlayer.item.gameObject.transform;
            Vector3 Direction = (targetObject.position - headObject.position).normalized;
            float angle = Vector3.SignedAngle(Direction, headForward.forward, headForward.up);

            if (angle < MaxAngle && angle > MinAngle)
            {
                if (!isLooking)
                {
                    isLooking = true;
                    lastRotation = headObject.rotation;
                }

                Quaternion targetRotation = Quaternion.LookRotation(targetObject.position - headObject.position);
                lastRotation = Quaternion.Slerp(lastRotation, targetRotation, lookSpeed * Time.deltaTime);

                headObject.rotation = lastRotation;
                headResetTimer = 0.5f;
            }
            else if (isLooking)
            {
                lastRotation = Quaternion.Slerp(lastRotation, headForward.rotation, lookSpeed * Time.deltaTime);
                headObject.rotation = lastRotation;
                headResetTimer -= Time.deltaTime;

                if (headResetTimer <= 0)
                {
                    headObject.rotation = headForward.rotation;
                    isLooking = false;
                }
            }
        }
    }

    //private void OnAnimatorIK()
    //{
    //    if (anim)
    //    {
    //        if (ikActive) 
    //        { 
    //            if (objTarget != null) 
    //            {
    //                anim.SetLookAtWeight(lookWeight);
    //                anim.SetLookAtPosition(objTarget.position);
    //            }
    //        }
    //        else
    //        {
    //            anim.SetLookAtWeight(0);
    //        }
    //    }
    //}
}
