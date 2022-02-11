using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plane : Entity
{

	public PlaneController controller;
	public Animator animator;
	public Aerodynamic aerodynamics;


    // Use this for initialization
    void Start 	() {
        controller = this.GetComponent<PlaneController>();
		aerodynamics = this.GetComponent<Aerodynamic>();

		rb = GetComponent<Rigidbody2D>();
		rb.inertia = aerodynamics.inertia;
		rb.velocity = new Vector2(4, 1);
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

		// Adjusts plane aerodynamic force based off wind contact
		rb.AddForce(windForce);

		// Brings force back to original aerodynamic force after being affected by the wind
		windForceDecay();

		if (rb.velocity.magnitude > 20) {
			rb.velocity = rb.velocity.normalized * 20;
		}
		animator.SetFloat("speed", rb.velocity.magnitude);
	}

	// returns the RigidBody for the Plane
	public Rigidbody2D getRigidBody() {
		return rb;
	}

	// Commits death on the plane and restarts the screen
	public void die() {
		//die and respawn
		// SceneManager.LoadScene("SampleScene"); 
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// Add collider for plane usually collision with obstacles to play death animations
	public void OnCollisionEnter2D(Collision2D other) 
	{
		// Check if collision is with Tree object
		if (other.collider.gameObject.CompareTag("Tree"))
		{
			// Call death method to respawn
			// TODO: Add an animation after collision before respawn for 
			//       better playability
			die();
		}
		if (other.collider.gameObject.CompareTag("Water"))
		{
			// Call death method to respawn
			// TODO: Add an animation after collision before respawn for 
			//       better playability
			die();
		}
	}

}
