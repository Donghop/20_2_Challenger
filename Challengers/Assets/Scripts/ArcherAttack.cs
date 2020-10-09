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

    private Ray ray;
    private RaycastHit hit;

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
        ChangeDirection();
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

    private void ChangeDirection() //공격, 대쉬 전 마우스 커서 방향으로 방향전환
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = hit.point;
            transform.LookAt(new Vector3(direction.x, transform.position.y, direction.z));
        }
    }
}
