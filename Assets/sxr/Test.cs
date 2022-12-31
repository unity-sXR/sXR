using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(CollisionHandler.Instance.ObjectsCollidersTouching(
                sxr.GetObject("LeftControllerOpenXR"), sxr.GetObject("RightControllerOpenXR")));
        }
        
        

    }

    
}
