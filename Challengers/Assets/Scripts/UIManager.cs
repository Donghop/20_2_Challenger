using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider mageHPBar;
    private MageAttack MA;
    private float curMageHP;

    private void Start()
    {
        MA = GameObject.Find("Mage").GetComponent<MageAttack>();
    }

    private void Update()
    {
        curMageHP = MA.curHP / MA.maxHP;
        mageHPBar.value = Mathf.Lerp(mageHPBar.value, curMageHP, Time.deltaTime * 10);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

}
