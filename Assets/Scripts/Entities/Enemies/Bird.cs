using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //gets command from its controller
        Vector2 command = behaviorController.GetMove();
        transform.position = command;

        // //adds on the windForce if there is any
        // rb.AddForce(windForce);

        // //decays the windforce
        // windForceDecay();
    }

    // Triggers when plane is under
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            ResourceBar.instance.collision(damage);
        }

    }
}
