using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 3f;
    public void Shoot(Vector3 direction)

    {
        this.direction = direction;
        
        Invoke("DestroyBullet", 5f);
    }

    public void DestroyBullet()
    {
        ObjectPool.ReturnObject(this);
    }

    void Update()
    {
        transform.Translate(direction*speed* Time.deltaTime);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ObjectPool.ReturnObject(this);
        }
          else  if (collision.transform.CompareTag("wall"))
        {
            ObjectPool.ReturnObject(this);
        }

    }
}
