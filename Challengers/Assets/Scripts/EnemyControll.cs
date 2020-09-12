using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : EnemyMeleeFSM
{
    public GameObject enemyCanvasGo;
   
    private void Start()
    {
       

    }

    private void Update()
    {

        
       
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag ("Player"))
        {
            enemyCanvasGo.GetComponent<EnemyHpBar>().currentHp -= 300f;
            currentHp -= 300f;
        }

       else if (collision.transform.CompareTag("wall")) 
        {
           
        }
    }
}
