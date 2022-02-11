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

    public Vector2 getWindForce()
    {
        return windForce;
    }

    //Brings wind force back to 0
    protected void windForceDecay() {
        if (tick > 60) {
            if (windForce.x > 0) {
			    windForce.x -= Mathf.Max(1, windForce.x);
		    } else if (windForce.x < 0) {
                windForce.x += Mathf.Max(1, -windForce.x);
            }
		    if (windForce.y > 0) {
			    windForce.y -= Mathf.Max(1, windForce.y);
		    } else if (windForce.y < 0) {
                windForce.y += Mathf.Max(1, -windForce.y);
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
