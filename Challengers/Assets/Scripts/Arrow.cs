using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;

    private float speed;

    private void Start()
    {
        speed = 3.0f;
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider coll)
    {
        Destroy(this.gameObject);
    }
}
