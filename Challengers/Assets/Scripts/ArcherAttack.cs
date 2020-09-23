using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    private PlayerCtrl pc;
    private Animator anim;
    public Transform spawnPoint;

    private bool isAttack;

    public GameObject arrow;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        anim = GetComponent<Animator>();
        isAttack = false;
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
        Instantiate(arrow, spawnPoint);
    }

    public void EndAttack()
    {
        pc.canMove = true;
    }
}
