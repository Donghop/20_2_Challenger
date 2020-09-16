using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Slider hpSlider;
    public Transform enemy;
    public float maxHp = 8000f;
    public float currentHp = 8000f;

    void Update()
    {
        
        hpSlider.value =Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime*5f);
    }
}
