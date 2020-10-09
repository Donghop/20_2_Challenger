using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageAttack : MonoBehaviour
{
    public Image tpCooldownImage;
    public Image flameStorm;
    private PlayerCtrl pc;
    private Animator anim;

    private bool isAttack;
    private bool canTP;
    private bool canFS;

    [SerializeField]
    private float chargeTime;
    private float tpCooldown;
    private float fsCooldown;

    private Ray ray;
    private RaycastHit hit;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        anim = GetComponent<Animator>();
        chargeTime = 0;
        tpCooldown = 8.0f;
        isAttack = false;
        canTP = true;
        canFS = true;
    }

    private void Update()
    {
        anim.SetBool("isAttack", isAttack);

        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime;
            pc.speed = 3.0f;
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            ChargeAttack(chargeTime);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canTP == true)
        {
            Teleport();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            FlameStrom();
        }
    }
    
    void ChargeAttack(float time)
    {
        isAttack = true;
        pc.canMove = false;

        anim.Play("MageAtk");
        ChangeDirection();

        if(time<2.0f)
        {
            Debug.Log("1단 공격");
        }
        else if(time>=2.0f&&time<4.0f)
        {
            Debug.Log("2단 공격");
        }
        else
        {
            Debug.Log("3단 공격");
        }

        chargeTime = 0;
        pc.speed = 5.0f;
        isAttack = false;
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

    public void EndAttack()
    {
        pc.canMove = true;
    }

    private void Teleport()
    {
        isAttack = true;
        pc.canMove = false;
        canTP = false;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePosition = hit.point;
            transform.position = new Vector3(mousePosition.x, mousePosition.y, mousePosition.z);
        }
        anim.Play("Teleport");
        StartCoroutine(CheckTPCooldown(tpCooldown));
    }

    public void EndTeleport()
    {
        pc.canMove = true;
        isAttack = false;
    }

    IEnumerator CheckTPCooldown(float cool) //대쉬 쿨타임 체크
    {
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            tpCooldownImage.fillAmount = ((1 - (cool / tpCooldown)));
            yield return null;
        }
        canTP = true;
        tpCooldownImage.fillAmount = 1;
    }

    private void FlameStrom()
    {
        isAttack = true;
        pc.canMove = false;
        canFS = false;        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePosition = hit.point;            
        }

        ChangeDirection();
        anim.Play("MageAtk");
        Instantiate(flameStorm, hit.transform.position, hit.transform.rotation);
        StartCoroutine(CheckFSCooldown(fsCooldown));
    }

    IEnumerator CheckFSCooldown(float cool) //대쉬 쿨타임 체크
    {
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            flameStorm.fillAmount = ((1 - (cool / fsCooldown)));
            yield return null;
        }
        canFS = true;
        flameStorm.fillAmount = 1;
    }
}
