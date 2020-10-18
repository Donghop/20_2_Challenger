using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;

    private float distance = 5.0f;
    private float height = 1.0f;
    private float rotationDamping;
    private float heightDamping;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        //transform.position = player.position + offset;

        var wantedRotationAngle = player.eulerAngles.y;
        var wantedHeight = player.position.y + height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = player.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(player);
    }
}
