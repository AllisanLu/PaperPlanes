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
        Vector2 displacement = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float desiredPitch = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

        float pitch = transform.eulerAngles.z;

        pitchCommand = Mathf.DeltaAngle(pitch, desiredPitch) * sensitivity * Mathf.Deg2Rad;
    }

    public float GetInput() {
        return pitchCommand;
    }
}
