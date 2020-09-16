using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotSpeed = 200.0f;

    private Rigidbody rb;
    private Transform tr;
    private Animator anim;
    private Vector3 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {      
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        anim.SetBool("isMove", moveDir != Vector3.zero);

        moveDir = new Vector3(h, 0, v).normalized;

        transform.position += moveDir * speed * Time.deltaTime;
        transform.LookAt(transform.position + moveDir);       
    }
}
