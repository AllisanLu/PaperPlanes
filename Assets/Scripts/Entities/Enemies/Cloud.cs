using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Enemy
{
    public float speedup;
    public GameObject cloud;

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("dying", false);
        //cloud = this.GetComponent<GameObject>();
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
            rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.Scale(new Vector2(speedup, speedup), rb.velocity);
            anim.SetBool("dying", true);
            Destroy(transform.parent.gameObject, 1f);
        }
        
    }

}
