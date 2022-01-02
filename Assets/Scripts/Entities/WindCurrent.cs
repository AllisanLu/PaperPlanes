using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCurrent : Entity
{
    private LineRenderer currentLine;

    public Vector2 position1;
    public Vector2 position2;
    public Vector2 direction;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLine = GetComponent<LineRenderer>();
        position1 = currentLine.GetPosition(0);
        position2 = currentLine.GetPosition(1);
        direction = position2 - position1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // maybe put this in plane instead?
    // void OnCollisionEnter(Collision col) 
    // {
    //     if(col.gameObject.name == "Plane") {

    //     }
    // }


    // returns the RigidBody for the Wind Current
	public Rigidbody2D getRigidBody() {
		return rb;
	}
    
    // returns the Line Renderer for the Wind Current
	public LineRenderer getCurrentLine() {
		return currentLine;
	}

}	

