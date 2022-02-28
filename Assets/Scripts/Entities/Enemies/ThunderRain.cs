using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderRain : Enemy
{
    public Animator anim;
    private int ticks;

    // Start is called before the first frame update
    void Start()
    {
        ticks = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim.GetBool("dying"))
        {
            Destroy(transform.gameObject, 0.2f);
        }
        ticks++;
        if (ticks % 100 < 50)
        {
            anim.SetBool("thunder", true);
        }
        else
        {
            anim.SetBool("thunder", false);
        }
    }

        // Triggers when plane is under
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (ticks % 100 < 11)
            {
                damage *= 2;
            } 
            other.GetComponent<Plane>().takeDamage(damage);
        }

    }

}
