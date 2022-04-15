using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : EnemyController
{
    public float speed;
    public  float zAngle;
    private Vector3 forward = new Vector3(-1,0,0);
    private Vector3 movement;
    private Vector3 direction;
    private Vector2 startPos;

    Bird bird;


    // Start is called before the first frame update
    void Start()
    {


        bird = GetComponent<Bird>();
        transform.Rotate(0,0,zAngle);
        movement = transform.rotation * forward;
        direction = movement * speed;
        //Debug.Log("bird direction" + movement);
        startPos = transform.position;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector2 endPos = startPos + (Vector2) direction * 10f;

        Gizmos.DrawLine(startPos, endPos);
    }
    // Moves bird to the left at a constant rate
    public override Vector2 GetMove() {

        if (bird.animator.GetBool("collide"))
        {
            return (Vector2)transform.position;
        }
        //return (Vector2) transform.position + (new Vector2(-1, 0) * speed);
        return (Vector2) transform.position + (Vector2) direction;
    }
}
