using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        behaviorController = GetComponent<SharkController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.collider.GetComponent<Plane>().takeDamage(damage);
        }

    }

    public Rigidbody2D getRigidBody() {
        return rb;
    }
}
