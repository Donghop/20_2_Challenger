using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealFollowCam : MonoBehaviour
{
    private Transform player;
    private Transform playerHead;
    private Transform camPoint;
    public float dist = 5.0f;
    public float height = 7.0f;
    public bool isCheck;

    private Transform tr;

    [Header("Wall Obstacle Setting")]
    public float heightAboveWall = 3.0f;
    public float distAboveWall = 3.0f;
    public float colliderRadius = 2.5f;
    public float overDamping = 3.0f;
    private float originHeight;
    private float originDist;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerHead = GameObject.FindWithTag("CamPoint").GetComponent<Transform>();
        tr = GetComponent<Transform>();
        originHeight = height;
        originDist = dist;
        isCheck = false;
        camPoint = player;
    }

    private void Update()
    {
        if (isCheck==true)
        {
            height = Mathf.Lerp(height, heightAboveWall, Time.deltaTime * overDamping);
            dist = Mathf.Lerp(dist, distAboveWall, Time.deltaTime * overDamping);
            camPoint = playerHead;
        }
        else if(isCheck==false)
        {
            height = Mathf.Lerp(height, originHeight, Time.deltaTime * overDamping);
            dist = Mathf.Lerp(dist, originDist, Time.deltaTime * overDamping);
            camPoint = player;
        }
    }

    private void LateUpdate()
    {
        tr.position = player.position - (Vector3.forward * dist) + (Vector3.up * height);
        tr.LookAt(camPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, colliderRadius);
    }
}
