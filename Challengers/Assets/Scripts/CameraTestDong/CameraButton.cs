using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButton : MonoBehaviour
{
    public TestCamerMove my_camera;
   

    public int target_index = 0;
    private void OnMouseDown()
    {
        my_camera.MoveCamera(target_index);

      
    }
}
   

