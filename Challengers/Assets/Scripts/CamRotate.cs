using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 200.0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        Vector3 dir = new Vector3(v, h, 0);

        transform.eulerAngles += dir * rotSpeed * Time.deltaTime;
    }
}
