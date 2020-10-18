using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandBag : MonoBehaviour
{
    public Text damage;
    public Transform textPosition;

    private void Update()
    {
        damage.transform.position = textPosition.position;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "BlueFireball")
        {
            damage.text = "80";
        }
        if (coll.tag == "FlameStorm")
        {
            damage.text = "50";
        }
        if (coll.tag == "Arrow")
        {
            damage.text = "80";
        }
    }
}
