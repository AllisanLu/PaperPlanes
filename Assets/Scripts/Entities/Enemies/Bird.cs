using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    private int lastHit = 0;
    //[SerializeField] public FMODUnity.EventReference reference1;
    public static FMOD.Studio.EventInstance Chirp;
    public bool chirped = false;


    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        Chirp = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Birb");
        startPos = transform.position;
     //   Physics2D.IgnoreLayerCollision(6, 7, true);
    }   
    void Update()
    {
        if(GetComponent<Renderer>().isVisible && !chirped)
        {   
            chirped = true;
            Chirp.start();

        }
    }
    public override void Move()
    {
        //gets command from its controller
        Vector2 command = behaviorController.GetMove();
        transform.position = command;
        //Chirp.start();
        animator.SetBool("collide", false);
        lastHit++;
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Vector2 endPos = startPos + (Vector2) (transform.rotation * new Vector3(-20,0,0));

        Gizmos.DrawLine(startPos, endPos);
    }

    // Triggers when plane is hitting the borb
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.collider.GetComponent<Plane>().takeDamage(damage);
            animator.SetBool("collide", true);
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
            lastHit = 0;
        }
        if (lastHit > 10)
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>(), false);
        }
    }
}
