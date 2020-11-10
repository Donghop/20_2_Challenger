using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGolem : MonoBehaviour
{
    private Transform player;
    private Vector3 playerPos;

    private float distance;

    public float speed;
    public float detectionRadius;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        speed = 5.0f;
        detectionRadius = 5.0f;
        Destroy(this.gameObject, 10.0f);
    }

    private void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        if (distance <= detectionRadius)
        {
            Vector3 move = (playerPos - transform.position).normalized;
            this.gameObject.transform.Translate(move * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            Destroy(this.gameObject);
        }
    }

}
