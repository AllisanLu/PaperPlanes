using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : Enemy
{
    void Start()
    {

    }
    public override void Move()
    {
        Vector2 command = behaviorController.GetMove();
        transform.position = command;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Plane>().takeDamage(damage);
        }
    }
}