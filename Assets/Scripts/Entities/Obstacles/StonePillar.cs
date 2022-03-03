using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePillar : Obstacle
{
    public int damageToStart;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Check for collision with plane (if we don't want instant death)
 /*   void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("Player"))
        {
            // if we want to not have this as instant death have player press a key to launch again at the cost of health
            ResourceBar.instance.collision(damageToStart);
        }
    } */
}
