using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : Enemy
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    
    // Triggers when plane is under
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            ResourceBar.instance.collision(damage);
        }
        
    }

}
