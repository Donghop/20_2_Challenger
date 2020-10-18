using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 0.5f);
    }
}
