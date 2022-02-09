using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSprout : Obstacle
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
        WindCurrent wind = other.GetComponent<WindCurrent>();
        if (wind != null)
        {
            if (wind.getForce() > 2)
            {
                //Play animation before destroying object
                Destroy(this.gameObject);
            }
        }
    }
}
