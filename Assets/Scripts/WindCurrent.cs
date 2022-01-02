using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCurrent : MonoBehaviour
{
    private Vector2 direction;

    //delete use this to find a good windforce
    public int force;
    // Start is called before the first frame update
    void Start()
    {
        //set direction on start here
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 getWindForce() {
        return direction * force;
    }

    public void setDirection(Vector2 direction) {
        this.direction = direction;
    }
}
