using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class GrabbableObject : MonoBehaviour
{
    bool grabbedLastFrame = false; 
    Vector3 lastControllerPos = new Vector3(); 
    bool usesGravity;

    void Update() {
        bool isCurrentlyGrabbed = false;
        
        GameObject[] controllers = new[]
            {sxr.GetObject("RightController"), sxr.GetObject("LeftController")};
        
        foreach(var controller in controllers)
            if (CollisionHandler.Instance.ObjectsCollidersTouching(this.gameObject, controller) 
                && sxr.CheckController(ControllerButton.Trigger, 0))
            {
                GetComponent<Rigidbody>().useGravity = false;

                if (grabbedLastFrame)
                    transform.position += controller.transform.position - lastControllerPos;
                lastControllerPos = controller.transform.position; 
                isCurrentlyGrabbed = true;
                break; }

        grabbedLastFrame = isCurrentlyGrabbed;
        GetComponent<Rigidbody>().useGravity = !isCurrentlyGrabbed && usesGravity; 
    }

    void Start() {
        if (GetComponent<Rigidbody>() == null)
            gameObject.AddComponent<Rigidbody>();

        usesGravity = GetComponent<Rigidbody>().useGravity; }
}
