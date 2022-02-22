using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refill : Item
{
    public Animator animator;
    public int additionalResources =0;

    // Start is called before the first frame update
    void Start()
    {
      animator.SetBool("Dying", false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //adds x resources to the resource bar when touched by player
            ResourceBar.instance.addResource(additionalResources);
            animator.SetBool("Dying", true);
            Destroy(transform.gameObject, 0.6f);
        }
    }
}
