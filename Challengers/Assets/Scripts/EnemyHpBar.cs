using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Slider hpSlider;
    public Transform enemy;
    public float maxHp = 1000f;
    public float currentHp = 1000f;

    void Update()
    {
        transform.position = enemy.position;
        hpSlider.value =Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime*5f);
    }
}
