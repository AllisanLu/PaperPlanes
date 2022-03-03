using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : EnemyController
{
    public float speed;
    Bird bird;

    // Start is called before the first frame update
    void Start()
    {
        bird = GetComponent<Bird>();
    }

    // Moves bird to the left at a constant rate
    public override Vector2 GetMove() {
        if (bird.animator.GetBool("collide"))
        {
            return (Vector2)transform.position;
        }
        return (Vector2) transform.position + (new Vector2(-1, 0) * speed);
    }
}
