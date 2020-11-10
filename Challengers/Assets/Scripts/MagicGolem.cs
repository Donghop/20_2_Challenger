using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicGolem : MonoBehaviour
{
    private Transform player;
    private Vector3 randomPoint;
    private Animator anim;
    private float speed;
    private int attackStack;
    private int laserStack;
    private bool canMove;

    public GameObject meteor;
    public GameObject meteorProjector;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        speed = 1.5f;
        attackStack = 0;
        laserStack = 0;
        canMove = true;
        randomPoint = new Vector3(Random.Range(-10f, 10f), 0.0f, Random.Range(-10f, 10f));
        //StartCoroutine(CheckRandomPoint());
        StartCoroutine(LightningCounter());
        StartCoroutine(PlayAttackPattern());
    }

    private void Update()
    {
        Vector3 moveDir = (randomPoint - transform.position).normalized;
        anim.SetBool("isMove", moveDir != Vector3.zero);
        if (canMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    IEnumerator LightningCounter()
    {
        Vector3 spawnPoint;
        for (int i = 0; i < 5; i++)
        {           
            spawnPoint = new Vector3(Random.Range(-10f, 10f), 12f, Random.Range(-10f, 10f));
            Instantiate(meteor, spawnPoint, meteor.transform.rotation);
            Instantiate(meteorProjector, spawnPoint, meteorProjector.transform.rotation);
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(LightningCounter());
    }

    //IEnumerator CheckRandomPoint()
    //{
    //    yield return new WaitForSeconds(4.0f);
    //    randomPoint = new Vector3(Random.Range(-10f, 10f), 0.0f, Random.Range(-10f, 10f));
    //    transform.LookAt(randomPoint);        
    //    StartCoroutine(CheckRandomPoint());
    //}

    IEnumerator PlayAttackPattern()
    {
        yield return new WaitForSeconds(4.0f);
        canMove = false;
        float distance = Vector3.Distance(player.position,transform.position);
        ChangeDirection();

        if (attackStack < 3)
        {
            if (distance <= 3.0f)
            {
                ShockWave();
            }
            else if (distance > 3.0f)
            {
                LaserAttack();
            }
        }
        else if(attackStack == 3)
        {
            ElectricShock();
        }
    }

    private void ShockWave()
    {
        attackStack++;
        anim.SetTrigger("ShockWave");
    }

    private void LaserAttack()
    {
        attackStack++;
        anim.SetTrigger("LaserAttack");
    }

    private void ElectricShock()
    {
        attackStack = 0;
        anim.SetTrigger("ElectricShock");
    }

    public void Self_Repairing()
    {

    }

    public void AttackEnd()
    {
        randomPoint = new Vector3(Random.Range(-10f, 10f), 0.0f, Random.Range(-10f, 10f));
        transform.LookAt(randomPoint);
        canMove = true;
        StartCoroutine(PlayAttackPattern());
    }

    public void CheckLaserStack()
    {
        if(laserStack<5)
        {
            laserStack++;
        }
        else
        {
            laserStack = 0;
            anim.SetTrigger("LaserEnd");
            randomPoint = new Vector3(Random.Range(-10f, 10f), 0.0f, Random.Range(-10f, 10f));
            transform.LookAt(randomPoint);
            canMove = true;
            StartCoroutine(PlayAttackPattern());
        }
    }

    private void ChangeDirection()
    {
        transform.LookAt(player);
    }
}
