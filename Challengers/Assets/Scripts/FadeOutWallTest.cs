using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutWallTest : MonoBehaviour
{
    private RealFollowCam RF;

    private void Start()
    {
        RF = GameObject.FindWithTag("MainCamera").GetComponent<RealFollowCam>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag=="Player")
        {
            RF.isCheck = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag=="Player")
        {
            RF.isCheck = false;
        }
    }
    //public GameObject wall;


    //private void OnTriggerEnter(Collider col)
    //{
    //    if(col.tag=="Player")
    //    {
    //        wall.SetActive(false);
    //    }
    //}

    //private void OnTriggerExit(Collider col)
    //{
    //    if (col.tag == "Player")
    //    {
    //        wall.SetActive(true);
    //    }
    //}
}
