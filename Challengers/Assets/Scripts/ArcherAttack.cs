using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    private PlayerCtrl pc;
    private Animator anim;

    public Transform spawnPoint;

    [SerializeField]
    private int attackStack;
    private bool isAttack;

    public GameObject arrow;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        anim = GetComponent<Animator>();
        isAttack = false;
        attackStack = 0;
    }

    private void Update()
    {
        anim.SetBool("isAttack", isAttack);

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        pc.canMove = false;
        anim.Play("ArcherAtk");       
    }

    private void ShootArrow()
    {
        if (attackStack < 2)
        {
            Instantiate(arrow, spawnPoint.position, spawnPoint.rotation);
            attackStack++;
        }
        else
        {
            attackStack = 0;
            Instantiate(arrow, spawnPoint.position, spawnPoint.rotation);          
        }
    }

    public void EndAttack()
    {
        pc.canMove = true;
        attackStack = 0;
    }
}
