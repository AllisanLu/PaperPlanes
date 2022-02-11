using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : Controller
{
[Range(0, 1)]
    public float sensitivity;
    private float pitchCommand;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 displacement = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        Plane plane = this.GetComponent<Plane>();
        Rigidbody2D rb = plane.getRigidBody();
        float angle = transform.rotation.eulerAngles.z;
        Vector2 windForce = plane.getWindForce();

        if (Mathf.Abs(windForce.magnitude) > 2) {
           // print("wind!");
            //pitch in direction of wind
            Vector2 displacement = windForce;
            float desiredPitch = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
            pitchCommand = Mathf.DeltaAngle(angle, desiredPitch) * sensitivity * Mathf.Deg2Rad;
            //print(pitchCommand);
        } else {
            // if the plane is tilted too low, tilt the plane up
            // if the plane is going the wrong direction (left) turn the plane around
            // else have the plane follow a slow decent 
            // print(angle);
/*            if (angle < 360 && angle > 270)
            {
                desiredAngle = 80;
            }
            else if (angle > 90 && angle <= 270)
            {
                desiredAngle = angle + 45;
            }
            else
            {
                desiredAngle = 280;
            }*/
            // print("desired: " + desiredAngle);
            // calculate the pitch and pitchcommand from displacement
            pitchCommand = Mathf.DeltaAngle(angle, 0) * sensitivity * Mathf.Deg2Rad;
            //print(pitchCommand);
        }
       
    }

    // Gives a float force for a Plane
    // returns a float with a pitch command for a Plane
    public override float GetAction() {
        return pitchCommand;
    }
}
