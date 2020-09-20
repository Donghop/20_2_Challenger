using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyControll : EnemyMeleeFSM
{
    public GameObject enemyCanvasGo;
    public GameObject doublePlay;
    private Transform tr;
    public Vector3 direct;
    public float TotalHp = 8000f;


    public List<GameObject> fireposition = new List<GameObject>();
    

    public GameObject EnemyParent;

    public Text hpCount;

    public int deathCount = 0;

    public float hitTest = 300f;

    

    private void Start()
    {
        
        tr = GetComponent<Transform>();
        int randomCount = Random.Range(1, 2);
        direct = new Vector3(randomCount, 0, randomCount);

       StartCoroutine(Fire());




    }

    private void Update()
    {


        tr.Translate(direct * moveSpeed * Time.deltaTime);


        if (currentHp <= 0)
        {
           
            deathCount += 1;
            this.gameObject.SetActive(false);

            if (!gameObject.activeSelf)
            {
              
                currentHp = 0;
                hitTest = 300;
                StopCoroutine(Fire());
                DoubleSlime();
            }
        }
    }
    private void DoubleSlime()
    {
        if (deathCount == 1)
        {
            maxHp = 1000f;
            currentHp = 1000f;
          
            this.gameObject.transform.localScale -= new Vector3(1, 1, 1);
            
            this.gameObject.SetActive(true);
            Instantiate(doublePlay, EnemyParent.transform);
            StartCoroutine(Fire());
        }
        else if (deathCount == 2)
        {
            maxHp = 500f;
            currentHp = 500f;

            this.gameObject.transform.localScale -= new Vector3(1, 1, 1);
            
            this.gameObject.SetActive(true);
            Instantiate(doublePlay, EnemyParent.transform);
            StartCoroutine(Fire());
        }
        else if (deathCount == 3)
        {
            maxHp = 250f;
            currentHp = 250f;


            this.gameObject.transform.localScale -= new Vector3(1, 1, 1);
            
            this.gameObject.SetActive(true);
            Instantiate(doublePlay, EnemyParent.transform);
            StartCoroutine(Fire());
        }
        else if (deathCount >= 4)
        {
            this.gameObject.SetActive(false);
            doublePlay.SetActive(false);
            StopAllCoroutines();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

       

        if (collision.transform.CompareTag ("Player"))
        {
            if (currentHp <= hitTest)
            {
           
                hitTest = currentHp;
                enemyCanvasGo.GetComponent<EnemyHpBar>().currentHp -= hitTest;
                currentHp -= hitTest;
                if (currentHp == hitTest)
                {
                    Debug.Log("cHp = hittestdamage");
                }

            }
            else
            {
                
                enemyCanvasGo.GetComponent<EnemyHpBar>().currentHp -= hitTest;
                currentHp -= hitTest;

                TotalHp = TotalHp - hitTest;
            }
            hpCount.text = enemyCanvasGo.GetComponent<EnemyHpBar>().currentHp+"/"+ 8000;

           
        }

       else if (collision.transform.CompareTag("wall") || collision.transform.CompareTag("Boss")) 
        {
            
            // 입사벡터를 알아본다. (충돌할때 충돌한 물체의 입사 벡터 노말값)
            Vector3 incomingVector = direct;
            incomingVector = incomingVector.normalized;
            // 충돌한 면의 법선 벡터를 구해낸다.
            Vector3 normalVector = collision.contacts[0].normal;
            // 법선 벡터와 입사벡터을 이용하여 반사벡터를 알아낸다.
            Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector); //반사각
            reflectVector = reflectVector.normalized;

            direct.x = reflectVector.x;
            direct.z = reflectVector.z;
            
        }

        
        
    }

    IEnumerator Fire()
    {

        while (true)
        {
            
            yield return new WaitForSeconds(2);

            for (int i = 0; i < 4; i++)
            {
                var bullet = ObjectPool.GetObject();
                bullet.transform.position = fireposition[i].transform.position;
                bullet.transform.rotation = fireposition[i].transform.rotation;
                bullet.Shoot(fireposition[i].transform.position);
            }
        }
    }



    /// <summary>
    /// 테스트용 마우스클릭 데미지
    /// </summary>
    private void OnMouseDown()
    {


        if (currentHp <= hitTest)
        {

            hitTest = currentHp;
            enemyCanvasGo.GetComponent<EnemyHpBar>().currentHp -= hitTest;
            currentHp -= hitTest;
            if (currentHp == hitTest)
            {
                Debug.Log("cHp = hittestdamage");
            }

        }
        else
        {
            
            enemyCanvasGo.GetComponent<EnemyHpBar>().currentHp -= hitTest;
            currentHp -= hitTest;

            TotalHp = TotalHp - hitTest;
        }
        hpCount.text = enemyCanvasGo.GetComponent<EnemyHpBar>().currentHp + "/" + 8000;
    }
    
}
