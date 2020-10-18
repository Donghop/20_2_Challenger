using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        Destroy(this.gameObject, 1.0f);
    }


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag !="Player")
        {
            Destroy(this.gameObject);
        }
    }
}
