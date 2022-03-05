using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : Enemy
{
    public override void Move()
    {
        Vector2 command = behaviorController.GetMove();
        transform.position = command;
    }

    // Triggers when plane is hitting the borb
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.collider.GetComponent<Plane>().takeDamage(damage);
        }
    }
}
