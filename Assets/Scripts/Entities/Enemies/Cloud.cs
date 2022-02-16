using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Enemy
{
    public float speedup;
    public GameObject cloud;
    
    // Start is called before the first frame update
    void Start()
    {
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
           // print(rb.velocity);
            rb.velocity = Vector2.Scale(new Vector2(speedup, speedup), rb.velocity);
            // print(rb.velocity);
            Destroy(transform.parent.gameObject);
        }
        
    }

}
