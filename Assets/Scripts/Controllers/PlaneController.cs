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

        //need to change displacement as im assuming plane direction changes
        //if plane is angled down do top left or close to ground
        //if plane is ok just wiggle????
        Plane plane = this.GetComponent<Plane>();
        Rigidbody2D rb = plane.getRigidBody();
        //find angle here
        float angle = transform.rotation.eulerAngles.z;
        Debug.Log("Angle: " + angle);
        Vector2 displacement;
        if (angle < 360 && angle > 270) {    // \
            displacement = new Vector2(0, cam.pixelHeight) - (Vector2) transform.position;
        } else if (angle > 90 || angle < 270) {      // - left
            displacement = new Vector2(cam.pixelWidth, -cam.pixelHeight) - (Vector2) transform.position;
        } else { // /
            displacement = new Vector2(0, -cam.pixelHeight) - (Vector2) transform.position;
        }
        
        float desiredPitch = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

        float pitch = transform.eulerAngles.z;

        pitchCommand = Mathf.DeltaAngle(pitch, desiredPitch) * sensitivity * Mathf.Deg2Rad;
    }

    public override float GetAction() {
        return pitchCommand;
    }
}
