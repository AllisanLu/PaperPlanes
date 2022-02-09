using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //set hasShield to true

        }

        //if other hits enemy

            //set hasShield to false
    }

}
