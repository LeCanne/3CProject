using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody rb;
    public Transform tr;
    public float speedMove, speedJump;
    public bool isJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector3()
    }

    //public float Move()
    //{


    //    return;
    //}
}
