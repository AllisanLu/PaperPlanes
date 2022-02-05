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

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            print("Hi");
            ResourceBar.instance.collision(damage);
        }
    }
}
