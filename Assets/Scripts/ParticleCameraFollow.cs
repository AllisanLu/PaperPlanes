using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        temp.x = target.position.x;
        transform.position = temp;
    }
}
