using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    private MageAttack MA;
    private AncientGolem AG;

    private void Start()
    {
        MA = GameObject.Find("Mage").GetComponent<MageAttack>();
        AG = GameObject.Find("TreeGolem").GetComponent<AncientGolem>();
    }

    public void MageHPDown()
    {
        MA.curHP *= 0.5f;
    }

    public void AGHPDown()
    {
        AG.curHP *= 0.5f;
    }
}
