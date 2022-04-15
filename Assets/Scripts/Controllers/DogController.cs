using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : EnemyController
{

    Dog dog;
    public float acceleration = 1;
    public float speed;
    public  float zAngle;
    private Vector3 forward = new Vector3(-1,0,0);
    private Vector3 movement;
    private Vector3 direction;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        dog = GetComponent<Dog>();
        transform.Rotate(0,0,zAngle);
        movement = transform.rotation * forward;
        direction = movement * speed;
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
    }

    //calculates a starting Velocity so that you can go as high and far as you want
    //results in a 0 vf for the y direction :D
    //it should follow closely to the gizmo
    //idk why it doesnt really fit perfectly, but its close enough :D
    public Vector2 getStartVelocity(float far, float height)
    {
        float y;
        float x;

       // float acceleration = dog.getRigidBody().gravityScale;


        y = Mathf.Sqrt(2 * acceleration * 2 * height * 3.75f);


        float time = 2 * y / acceleration;

        x = far * 10.2f / time;

        return new Vector2(x, y);
    }


    public override Vector2 GetMove() {
        //return (Vector2) transform.position + (new Vector2(-1, 0) * speed);
        return (Vector2) transform.position + (Vector2) direction;
    }
} 
