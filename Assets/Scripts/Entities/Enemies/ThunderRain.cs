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
    }

    // Triggers when plane is under
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (ticks % 100 < 50)
            {
                print("should be thundering :(");
                anim.SetBool("thunder", true);
                damage *= 2;
            } else
            {
                anim.SetBool("thunder", false);
            }
            other.GetComponent<Plane>().takeDamage(damage);
        }

    }

}
