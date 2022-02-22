using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSprout : Obstacle
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("dying", false);
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
                anim.SetBool("dying", true);
                //Play animation before destroying object
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
}
