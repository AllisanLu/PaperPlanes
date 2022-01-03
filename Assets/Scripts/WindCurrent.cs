using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCurrent : MonoBehaviour
{

    private LineRenderer currentLine;

    public Vector2 position1;
    public Vector2 position2;
    public Vector2 direction = new Vector2(0, 0);

    public BoxCollider col;

    public float force = 4;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        currentLine = GetComponentInParent<LineRenderer>();
        position1 = currentLine.GetPosition(0);
        position2 = currentLine.GetPosition(1);
        direction = position2 - position1;
    }

    // Update is called once per frame
    void Update()
    {
        //tick here and then when enuff ticks just remove the wind
    }

    public Vector2 getWindForce() {
        return direction.normalized * force;
    }

    public void setForce(float force) {
        this.force = force;
    }

    public void setDirection(Vector2 direction) {
        this.direction = direction;
    }
    
    // returns the Line Renderer for the Wind Current
	public LineRenderer getCurrentLine() {
		return currentLine;
	}

    // void OnTriggerEnter2D(Collider2D other) {
    //     //print(other.GetComponent<Plane>());
    //     if (other.GetComponent<Entity>()) {
    //         Entity hit = other.GetComponent<Entity>();
    //         Vector2 windforce = getWindForce();
    //         hit.setWindForce(windforce);
    //     }
    // }
}
