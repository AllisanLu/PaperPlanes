using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : Enemy
{
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 command = behaviorController.GetMove();
        print("current: " + transform.position + " later: " + command);
        transform.position = command;
    }
}
