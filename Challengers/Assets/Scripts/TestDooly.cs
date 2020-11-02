using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDooly : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBullet(1.0f));
    }

    IEnumerator SpawnBullet(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
        StartCoroutine(SpawnBullet(delay));
    }
}
