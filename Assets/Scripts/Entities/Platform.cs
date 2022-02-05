using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Triggers when plane is on platform
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Landed on platform");
            // cutscene
        }
    }
}
