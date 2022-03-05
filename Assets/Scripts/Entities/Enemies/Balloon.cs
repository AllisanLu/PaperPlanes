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

    public override void Move()
    {
        //gets command from its controller
        Vector2 command = behaviorController.GetMove();
        transform.position = command;

        //adds on the windForce if there is any
        rb.AddForce(windForce);

        //decays the windforce
        windForceDecay();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            Plane plane = other.collider.GetComponent<Plane>();
            Vector2 displacement = plane.transform.position - this.transform.position;
            Vector2 balloonForce = -(displacement.normalized) * 250;
            plane.getRigidBody().AddForce(balloonForce);

            other.collider.GetComponent<Plane>().takeDamage(damage);

            //balloon pop animation
            animator.SetBool("dead", true);
        }
    }
}
