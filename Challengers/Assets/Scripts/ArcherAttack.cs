using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherAttack : MonoBehaviour
{
    private PlayerCtrl pc;
    private Animator anim;

    public Transform normalAttackPoint;
    public Transform AEAttackPoint;
    public Transform doubleAttackPoint1;
    public Transform doubleAttackPoint2;
    public Image aeCooldownImage;
    public Image dashCooldownImage;

    [SerializeField]
    private int attackStack;
    private bool isAttack;
    private bool isDash;
    private bool canAE;
    private bool canDash;

    public GameObject arrow;
    public GameObject arrowExplosion; 

    private Ray ray;
    private RaycastHit hit;

    private float aeCooldown;
    private float dashCooldown;
    private float dashTime;
    private float dashSpeed;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        anim = GetComponent<Animator>();
        isAttack = false;
        isDash = false;
        canAE = true;
        canDash = true;
        dashSpeed = 15.0f;
        dashTime = 0.3f;
        dashCooldown = 8.0f;
        aeCooldown = 3.0f;
        attackStack = 0;
    }

    private void Update()
    {
        anim.SetBool("isAttack", isAttack);
        anim.SetBool("isDash", isDash);

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true) //대쉬
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.F) && canAE == true)
        {
            ArrowExplosion();
        }
    }

    private void Attack()
    {
        //isAttack = true;
        ChangeDirection();
        pc.canMove = false;
        anim.Play("ArcherAtk");       
    }

    private void ShootArrow()
    {
        if (attackStack < 2)
        {
            Instantiate(arrow, normalAttackPoint.position, normalAttackPoint.rotation);
            attackStack++;
        }
        else
        {
            attackStack = 0;
            Instantiate(arrow, doubleAttackPoint1.position, doubleAttackPoint1.rotation);
            Instantiate(arrow, doubleAttackPoint2.position, doubleAttackPoint2.rotation);
        }
    }
    
    public void EndAttack()
    {
        //isAttack = false;
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

    private void ArrowExplosion()
    {
        //isAttack = true;
        canAE = false;
        pc.canMove = false;
        ChangeDirection();
        anim.Play("ArcherSkill");
        StartCoroutine(CheckAECooldown(aeCooldown));
    }

    public void ShootExplosion()
    {
        Instantiate(arrowExplosion, AEAttackPoint.position, AEAttackPoint.rotation);
    }

    public void EndSkill()
    {
        //isAttack = false;
        pc.canMove = true;
    }

    private void Dash() //대쉬
    {
        canDash = false;
        pc.canMove = false;
        isDash = true;
        isAttack = false;
        ChangeDirection();
        anim.Play("ArcherDash");
        StartCoroutine(CheckDashTime(dashTime));
        StartCoroutine(CheckDashCooldown(dashCooldown));
    }

    public void EndArcherDash() //대쉬 종료
    {
        isDash = false;
        pc.canMove = true;
    }

    IEnumerator CheckAECooldown(float cool) //스킬 쿨타임 체크
    {
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            aeCooldownImage.fillAmount = ((1 - (cool / aeCooldown)));
            yield return null;
        }
        canAE = true;
        aeCooldownImage.fillAmount = 1;
    }

    IEnumerator CheckDashCooldown(float cool) //대쉬 쿨타임 체크
    {
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            dashCooldownImage.fillAmount = ((1 - (cool / dashCooldown)));
            yield return null;
        }
        canDash = true;
        dashCooldownImage.fillAmount = 1;
    }

    IEnumerator CheckDashTime(float dash)
    {
        while (dash > 0.0f)
        {
            dash -= Time.deltaTime;
            transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
