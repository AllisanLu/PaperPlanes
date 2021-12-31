using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //target: the object to follow (gets positional information about the target)
    public Transform target;
    private int cameraOffset = 8;
    // Start is called before the first frame update
    
    // LateUpdate is called after movement
    private void LateUpdate()
    {
        Vector3 temp = transform.position;
        temp.x = target.position.x + cameraOffset;
        temp.y = target.position.y;
        transform.position = temp;
    }
}