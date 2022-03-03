using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : Enemy
{
    public Animator anim;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim.GetBool("dying")) {
            Destroy(transform.gameObject, 0.2f);
        }
    }
    
    // Triggers when plane is under
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Plane>().takeDamage(damage);
        }
        
    }

}
