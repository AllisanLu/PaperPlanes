using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private bool activated;
    void Start()
    {
        activated = false;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            // new checkpoint respawn is the same x position as the gameobject
            // y position is 15f (original position)
            Vector3 newPos = new Vector3(this.gameObject.transform.position.x, 15f, 0);
            // set checkpoint position to new position

            CheckpointManager.planePosition = newPos;
            if (!activated) {
                anim.Play("shrine_fill");
            }
        }
    }
}
