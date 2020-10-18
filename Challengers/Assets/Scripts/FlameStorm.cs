using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStorm : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }
}
