using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : Controller
{
[Range(0, 1)]
    public float sensitivity;
    private float pitchCommand;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 displacement = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        Plane plane = this.GetComponent<Plane>();
        Rigidbody2D rb = plane.getRigidBody();
        float angle = transform.rotation.eulerAngles.z;

        //Calculate displacement based on how the plane is angled
        Vector2 displacement;
        
        // if the plane is tilted too low, tilt the plane up
        // if the plane is going the wrong direction (left) turn the plane around
        // else have the plane follow a slow decent 
        if (angle < 360 && angle > 270) {    
            displacement = new Vector2(0, cam.pixelHeight) - (Vector2) transform.position;
        } else if (angle > 90 || angle < 270) {      
            displacement = new Vector2(cam.pixelWidth, -cam.pixelHeight) - (Vector2) transform.position;
        } else { 
            displacement = new Vector2(0, -cam.pixelHeight) - (Vector2) transform.position;
        }
        
        // calculate the pitch and pitchcommand from displacement
        float desiredPitch = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        float pitch = transform.eulerAngles.z;
        pitchCommand = Mathf.DeltaAngle(pitch, desiredPitch) * sensitivity * Mathf.Deg2Rad;
    }

    // Gives a float force for a Plane
    // returns a float with a pitch command for a Plane
    public override float GetAction() {
        return pitchCommand;
    }
}
