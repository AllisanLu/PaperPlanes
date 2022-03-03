using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    private int lastHit = 0;
    // Start is called before the first frame update
    void Start()
    {
     //   Physics2D.IgnoreLayerCollision(6, 7, true);

    }

    public override void Move()
    {
        //gets command from its controller
        Vector2 command = behaviorController.GetMove();
        transform.position = command;

        animator.SetBool("collide", false);
        lastHit++;
    }

    // Triggers when plane is hitting the borb
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.collider.GetComponent<Plane>().takeDamage(damage);
            animator.SetBool("collide", true);
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
            lastHit = 0;
        }
        if (lastHit > 10)
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>(), false);
        }
    }
}
