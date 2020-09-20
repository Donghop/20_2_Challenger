using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed;
    public bool canMove;

    private Transform tr;
    private Animator anim;
    private Vector3 moveDir;

    void Start()
    {
        speed = 5.0f;
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        canMove = true;
    }

    void Update()
    {
        if (canMove == true)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            anim.SetBool("isMove", moveDir != Vector3.zero);

            moveDir = new Vector3(h, 0, v).normalized;

            tr.position += moveDir * speed * Time.deltaTime;
            tr.LookAt(tr.position + moveDir);
        }        
    }
}
