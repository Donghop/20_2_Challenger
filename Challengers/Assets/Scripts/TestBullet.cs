using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    private Transform player;
    private float speed;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        speed = 3.0f;
        Destroy(this.gameObject, 5.0f);
    }

    void Update()
    {
        Vector3 move = (player.transform.position - this.gameObject.transform.position).normalized;
        this.gameObject.transform.Translate(move * speed * Time.deltaTime);
    }
}
