using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightAttack : MonoBehaviour
{
    public Image dashCooldownImage;
    private Animator anim;
    private PlayerCtrl pc;

    private bool comboPossible;
    private int comboStep;
    private bool canDash;
    private bool isAttack;
    private bool isDash;
    private float dashCooldown;
    private float dashSpeed;
    private float dashTime;

    private Ray ray;
    private RaycastHit hit;


    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        anim = GetComponent<Animator>();
        dashSpeed = 15.0f;
        dashTime = 0.3f;
        comboStep = 0;
        canDash = true;
        dashCooldown = 6.0f;
        isAttack = false;
        isDash = false;
    }

    private void Update()
    {
        anim.SetBool("isAttack", isAttack);
        anim.SetBool("isDash", isDash);
        if (Input.GetMouseButtonDown(0)) //공격
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true) //대쉬
        {
            Dash();
        }
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

    private void Attack() //공격
    {
        pc.canMove = false;
        isAttack = true;

        if (comboStep == 0)
        {
            ChangeDirection();
            anim.Play("KnightAtk1");
            comboStep = 1;
        }
        else if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

    public void Combo() //콤보 공격
    {
        if (comboStep == 2)
        {
            ChangeDirection();
            anim.Play("KnightAtk2");
        }
        else if (comboStep == 3)
        {
            ChangeDirection();
            anim.Play("KnightAtk3");
        }
    }

    public void ComboPossible() //콤보 가능 시점 체크
    {
        comboPossible = true;
    }

    public void ComboReset() //콤보 종료
    {
        pc.canMove = true;
        isAttack = false;
        comboPossible = false;
        comboStep = 0;
    }

    private void Dash() //대쉬
    {
        canDash = false;
        pc.canMove = false;
        isDash = true;
        isAttack = false;
        ChangeDirection();
        anim.Play("Dash");
        StartCoroutine(CheckDashTime(dashTime));
        StartCoroutine(CheckDashCooldown(dashCooldown));
    }

    public void EndDash() //대쉬 종료
    {
        isDash = false;
        comboPossible = false;
        comboStep = 0;
        pc.canMove = true;
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
        while(dash>0.0f)
        {
            dash -= Time.deltaTime;
            transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
