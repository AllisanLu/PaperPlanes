using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : EnemyController
{
    
    Vector2 startPos;

    Shark shark;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        shark = GetComponent<Shark>();
        startPos = this.gameObject.transform.position;
        rb = shark.getRigidBody();
        rb.velocity = new Vector2(10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0) {
            transform.position = startPos;
            rb.velocity = new Vector2(10, 10);
        }
    }

}
