using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloud : Enemy
{
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //gets command from its controller
        Vector2 command = behaviorController.GetMove();
        Debug.Log(command);
        transform.position = command;

        Debug.Log(windForce);
        //adds on the windForce if there is any
        rb.AddForce(windForce);

        //decays the windforce
        windForceDecay();
    }
}
