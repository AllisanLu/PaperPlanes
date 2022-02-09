using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shield;
    private bool isActive;


    // Start is called before the first frame update
    //start out with shield on screen, while player has no shield
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if player hits the shield,
    //the player gains the shield which vanishes from the screen
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(transform.gameObject);
            isActive = true; 
            print("shield hit");
        }
    }

    public bool IsActive
    {
        get 
        {
            return isActive;
        }
        set 
        {
            isActive = value;
        }
    }
}
