using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 10f;
    public void Shoot(Vector3 direction)
    {
       
        this.direction = direction;
        
       
    }

  

    void Update()
    {
        transform.Translate(Vector3.forward*speed* Time.deltaTime);
        
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
