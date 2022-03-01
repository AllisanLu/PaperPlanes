using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shield;

    private SpriteRenderer renderer;
    private bool isActive;

    // Start is called before the first frame update
    //start out with shield on screen, while player has no shield
    void Start()
    {
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(255f, 255f, 255f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if player hits the shield,
    //the player gains the shield which ideally vanishes from the screen
    //doesn't vanish right now
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isActive = true; 
            //make shield child class of parent class
            shield.transform.parent = other.transform;
            renderer.color = new Color(255f, 255f, 255f, 0.5f);
            shield.transform.localPosition = new Vector3(0, 0, 0);
            Plane plane = other.GetComponent<Plane>();
            plane.setShield(this);
        }
    }

   public bool IsActive()
    {
        return isActive;
    }
    public void setIsActive(bool active)
    {
        this.isActive = active;
    }
}

