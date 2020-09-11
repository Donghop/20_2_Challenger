using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public GameObject enemyCanvasGo;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag ("Player"))
        {
            enemyCanvasGo.GetComponent<EnemyHpBar>().currentHp -= 300f;
        }
    }
}
