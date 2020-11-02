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

    public float distance;
    private float attackDelay;
    public float speed;
    public int attackStack;
    public int shockwaveStack;


    private bool isActivate;
    private bool canMove;
    private bool tracing;
    

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
        attackDelay = 2.0f;
        speed = 2.2f;
        attackStack = 0;
        shockwaveStack = 0;
    }

    private void Update()
    {
        anim.SetBool("isMoving", tracing);
        distance = Vector3.Distance(player.position, transform.position);

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
            moveDir = (player.transform.position - this.gameObject.transform.position).normalized;
            tr.position += moveDir * speed * Time.deltaTime;
            tr.LookAt(tr.position + moveDir);
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
        Debug.Log("골렘 소환!");
        StartCoroutine(PlayAttackPattern(attackDelay));
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
