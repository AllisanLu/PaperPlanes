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
        if (GetComponent<Renderer>().isVisible)
        {
            float horizontal = -8f;
/*            float y = 0.05f * Mathf.Sin(Time.time * 4) + transform.position.y;
            transform.position = new Vector2(horizontal * Time.deltaTime + transform.position.x, y);*/
           transform.position = new Vector2(horizontal * Time.deltaTime + transform.position.x, transform.position.y);
        }  
    }
}
