using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected Vector2 windForce;

    private int tick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Setter for wind force
    public void setWindForce(Vector2 force) {
        windForce = force;
    }

    //Brings wind force back to 0
    protected void windForceDecay() {
        if (tick > 60) {
            if (windForce.x > 0) {
			    windForce.x -= 1;
		    } else if (windForce.x < 0) {
                windForce.x += 1;
            }
		    if (windForce.y > 0) {
			    windForce.y -= 1;
		    } else if (windForce.y < 0) {
                windForce.y += 1;
            }
            tick = 0;
        }
        tick++;
    }

    //Checks for collision with wind current
    void OnTriggerEnter2D(Collider2D other) {
        WindCurrent wind = other.GetComponent<WindCurrent>();
        if (wind != null)
        {
            windForce = wind.getWindForce();
        }
    }
}
