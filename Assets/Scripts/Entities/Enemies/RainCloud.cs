using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloud : Enemy
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
            Debug.Log("Plane under cloud");
        }
        
    }

}
