using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSprout : Obstacle
{
    public static FMOD.Studio.EventInstance Spout;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Spout = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Splash");

        Physics2D.IgnoreLayerCollision(6, 7, true);
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
                die();
                Spout.start();

            }
        } 
    }
    public void die()
    {
        //get rid of collider
        Destroy(GetComponent<Collider2D>());
        anim.SetBool("dying", true);
    }
}
