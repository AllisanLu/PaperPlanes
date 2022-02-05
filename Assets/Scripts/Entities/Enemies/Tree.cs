using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check for collision with plane (if we don't want instant death)
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            // do same damage as raincloud rain
            ResourceBar.instance.collision(damage);
        }
    }
}
