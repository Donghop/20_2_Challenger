using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : EnemyMeleeFSM
{
    public GameObject enemyCanvasGo;
    public GameObject doublePlay;
    public Transform tr;
    public Vector3 direct = new Vector3(1,0,1);
    private void Start()
    {

        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        tr.Translate(direct * moveSpeed * Time.deltaTime);
        
       
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
         
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

       

        if (collision.transform.CompareTag ("Player"))
        {
            enemyCanvasGo.GetComponent<EnemyHpBar>().currentHp -= 300f;
            currentHp -= 300f;
        }

       else if (collision.transform.CompareTag("wall")) 
        {
            Debug.Log("wall check");
            // 입사벡터를 알아본다. (충돌할때 충돌한 물체의 입사 벡터 노말값)
            Vector3 incomingVector = direct;
            incomingVector = incomingVector.normalized;
            // 충돌한 면의 법선 벡터를 구해낸다.
            Vector3 normalVector = collision.contacts[0].normal;
            // 법선 벡터와 입사벡터을 이용하여 반사벡터를 알아낸다.
            Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector); //반사각
            reflectVector = reflectVector.normalized;

            direct = reflectVector;
        }
    }
}
