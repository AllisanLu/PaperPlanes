using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : EnemyController
{

    Shark shark;
    public float acceleration = 1;


    // Start is called before the first frame update
    void Start()
    {
        shark = GetComponent<Shark>();
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

       // float acceleration = shark.getRigidBody().gravityScale;

        y = Mathf.Sqrt(2 * acceleration * 2 * height * 3.75f);

        float time = 2 * y / acceleration;

        x = far * 10.2f / time;

        return new Vector2(x, y);
    }
}
