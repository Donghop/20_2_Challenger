using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealFollowCam : MonoBehaviour
{
    private Transform player;
    public float dist = 10.0f;
    public float height = 5.0f;
    public float smoothFollow = 5.0f;

    private Transform tr;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        tr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        
    }
}
