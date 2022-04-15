using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static FMOD.Studio.EventInstance Check;

    // Start is called before the first frame update
    private Animator anim;
    private bool activated;
    void Start()
    {
        Check = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Checkpoint");
        activated = false;
        anim = gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            // new checkpoint respawn is the same x position as the gameobject
            // y position is 15f (original position)
            Vector3 newPos = new Vector3(this.gameObject.transform.position.x, 15f, 0);
            ResourceBar.instance.maxResource();
            // set checkpoint position to new position

            CheckpointManager.planePosition = newPos;
            if (!activated) {
                anim.Play("shrine_fill");
                Check.start();

            }
        }
    }
}
