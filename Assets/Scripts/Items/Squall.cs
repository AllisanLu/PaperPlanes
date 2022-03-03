using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squall : Item
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //activates squall and removes from screen when hit
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ResourceBar.instance.getSquall().setSquallActive();
            anim.SetBool("dying", true);
        }
    }
}
