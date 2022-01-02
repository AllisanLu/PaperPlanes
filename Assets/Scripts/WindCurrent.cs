using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCurrent : MonoBehaviour
{

    private LineRenderer currentLine;

    public Vector2 position1;
    public Vector2 position2;
    public Vector2 direction = new Vector2(0, 0);

    private Rigidbody2D rb;

    //delete use this to find a good windforce
    public int force;
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

    public Vector2 getWindForce() {
        return direction * force;
    }

    public void setDirection(Vector2 direction) {
        this.direction = direction;
    }

    public Rigidbody2D getRigidBody() {
		return rb;
	}
    
    // returns the Line Renderer for the Wind Current
	public LineRenderer getCurrentLine() {
		return currentLine;
	}
}
