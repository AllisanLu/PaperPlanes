using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    int x;
    // Start is called before the first frame update
    void Start()
    {
        x = 1;

        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = -1.5f;
        float y = -0.1f * (Mathf.Sin(Time.deltaTime * 3f));
        transform.position = transform.position + new Vector3(horizontal * Time.deltaTime, y, 0);
        
        

        
        
    }
}
