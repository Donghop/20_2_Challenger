using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientGolem : MonoBehaviour
{
    private Transform player;
    private Transform tr;
    private CameraShake cs;
    private Animator anim;
    private Vector3 moveDir;
    private Rigidbody rb;
    public GameObject testProjector; //테스트
    public GameObject miniGolem;
    public GameObject furyZone;

    public float distance;
    private float attackDelay;
    public float speed;
    public float MaxHP;
    public float curHP;
    public int attackStack;
    public int shockwaveStack;


    private bool isActivate;
    private bool canMove;
    private bool tracing;
    private bool isFury;

    private Vector3 testPosition;
    private Vector3 playerPos;
    

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        cs = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
        anim = GetComponent<Animator>();
        isActivate = false;
        canMove = false;
        tracing = false;
        isFury = false;
        attackDelay = 2.0f;
        speed = 2.2f;
        attackStack = 0;
        shockwaveStack = 0;
        MaxHP = 6000;
        curHP = 6000;
    }

    private void Update()
    {
        anim.SetBool("isMoving", tracing);
        distance = Vector3.Distance(player.position, transform.position);
        playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        testPosition = new Vector3(player.position.x, 5.0f, player.position.z); //테스트

        if (distance < 8.0f && isActivate == false)
        {
            isActivate = true;
            anim.SetTrigger("GolemActivate");
            StartCoroutine(cs.Shake(1.0f, 0.5f));           
        }

        if (distance < 1.5f)
        {
            tracing = false;
        }
        else
        {
            tracing = true;
        }

        if (canMove == true && tracing == true)
        {
            moveDir = (playerPos - transform.position).normalized;
            tr.position += moveDir * speed * Time.deltaTime;
            tr.LookAt(tr.position + moveDir);
        }

        if(curHP<=MaxHP/2 && isFury==false)
        {
            isFury = true;
            GolemFury();
        }
    }

    IEnumerator PlayAttackPattern(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (distance < 5.0f && attackStack != 2 && shockwaveStack != 3)
        {
            Swing();
        }
        else if(distance >= 5.0f && attackStack != 2 && shockwaveStack != 3)
        {
            Earthquake();
        }        
        else if(attackStack == 2 && shockwaveStack != 3)
        {
            ShockWave();
        }
        else
        {
            SpawnMiniGolem();
        }
    }

    private void Swing()
    {
        attackStack++;
        anim.SetTrigger("Swing");
    }

    private void Earthquake()
    {
        attackStack++;
        anim.SetTrigger("Earthquake");
        Instantiate(testProjector, testPosition, testProjector.transform.rotation);
    }

    private void ShockWave()
    {
        shockwaveStack++;
        anim.SetTrigger("ShockWave");
        attackStack = 0;
    }

    private void SpawnMiniGolem()
    {
        shockwaveStack = 0;
        for(int i=0;i<3;i++)
        {
            float randomX = Random.Range(-10f, 10f);
            float randomZ = Random.Range(-10f, 10f);
            Instantiate(miniGolem, new Vector3(randomX, 0.5f, randomZ),Quaternion.identity);
        }
        StartCoroutine(PlayAttackPattern(attackDelay));
    }

    private void GolemFury()
    {
        for (int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(-10f, 10f);
            float randomZ = Random.Range(-10f, 10f);
            Instantiate(furyZone, new Vector3(randomX, 0.0f, randomZ), Quaternion.identity);
        }
    }

    public void Activate()
    {
        canMove = true;
        tracing = true;
        StartCoroutine(PlayAttackPattern(attackDelay));
    }

    public void StartAttack()
    {
        canMove = false;
    }

    public void EndAttack()
    {
        canMove = true;
        StartCoroutine(PlayAttackPattern(attackDelay));
    }    
}
