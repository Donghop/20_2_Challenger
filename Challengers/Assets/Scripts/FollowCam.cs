using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
