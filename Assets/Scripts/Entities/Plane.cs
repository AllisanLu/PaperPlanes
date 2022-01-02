using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class Plane : Entity
{
//private Rigidbody2D rb;

	public PlaneController controller;

	public Aerodynamic aerodynamics;


    // Use this for initialization
    void Start () {
        controller = this.GetComponent<PlaneController>();
		aerodynamics = this.GetComponent<Aerodynamic>();

		rb = GetComponent<Rigidbody2D>();
		rb.inertia = aerodynamics.inertia;
		rb.velocity = new Vector2(3, -1);
	}

	// Called once per frame
	void FixedUpdate() {

		Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		
		//if the plane is below the screen it dies
		if (transform.position.y < 0) {
			die();
		}
        // Body frame velocity
		aerodynamics.bodyVelocity = transform.InverseTransformVector(rb.velocity);

        // Angle of attack
		aerodynamics.alpha = Mathf.Atan2(-aerodynamics.bodyVelocity.y, aerodynamics.bodyVelocity.x);

        // Aerodynamic force
        Vector2 force = aerodynamics.aeroForce();
		rb.AddForce(force);

		// Get torque from controller
		float pitchCommand = controller.GetAction();

		rb.AddTorque(pitchCommand * aerodynamics.controlStrength * aerodynamics.bodyVelocity.sqrMagnitude);

        // Stability torque
		rb.AddTorque(-aerodynamics.alpha * aerodynamics.stability * aerodynamics.bodyVelocity.sqrMagnitude);

        // Damping torque
		rb.AddTorque(-aerodynamics.damping * rb.angularVelocity * Mathf.Deg2Rad);

		rb.AddForce(windForce);

	}

	// returns the RigidBody for the Plane
	public Rigidbody2D getRigidBody() {
		return rb;
	}

	// Commits death on the plane and restarts the screen
	public void die() {
		//die and respawn
		SceneManager.LoadScene("SampleScene"); 
	}

	private void OnTriggerEnter2D(Collider2D other) {
		//update windForce here
		Debug.Log("weeee");
	}
}
