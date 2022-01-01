using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aerodynamic : MonoBehaviour
{
	public float span = .7f;
	public float cord = 0.3f;
	private float AR;
	public float zeroAngleLift = 0.1f;
	public float zeroAngleDrag = 0.01f;
	public float airDensity = 1.225f;
    public float controlStrength = 0.01f;
    public float stability = 0.1f;
    public float damping = 0.1f;
    public float inertia = 1f;

    [HideInInspector]
	private float alphaCrit = Mathf.PI / 12;

    [HideInInspector]
    public float alpha;
    
    [HideInInspector]
    public Vector2 bodyVelocity;


    void Start() {
        AR = Mathf.Pow(span, 2) / cord;
    }

	// Calculates the aerodynamic force on an object
	// returns a Vector2 of the force 
    public Vector2 aeroForce() {

        // Calculate lift coefficient
		float lift_coeff = 0;
		float alphaMax = Mathf.PI / 6;
		if (Mathf.Abs(alpha) <= alphaCrit) {
            //Thin airfoil theory
			lift_coeff = Mathf.PI*2*alpha + zeroAngleLift;

		} else if (Mathf.Abs(alpha) <= alphaMax) {
            // Janky stall code. No real theory
			if (alpha > 0) {
				float clmax = Mathf.PI*2*alphaCrit + zeroAngleLift;
				lift_coeff = clmax * (1 - Mathf.Pow((alpha - alphaCrit) / (alphaMax - alphaCrit), 2));
				
			} else {
				float clmin = -Mathf.PI*2*alphaCrit + zeroAngleLift;
				lift_coeff = clmin * (1 - Mathf.Pow((alpha + alphaCrit) / (alphaMax - alphaCrit), 2));
			}
		}
		
        // Calculate drag coefficient
		float drag_coeff = zeroAngleDrag + Mathf.Pow(Mathf.PI*2*alpha + zeroAngleLift,2) / (Mathf.PI * AR);
		if (Mathf.Abs(alpha) > alphaMax) {
			drag_coeff = zeroAngleDrag + Mathf.Pow(Mathf.PI*2*alphaMax + zeroAngleLift,2) / (Mathf.PI * AR);
		}

        // Drag and Lift forces
		float D = drag_coeff * 0.5f * airDensity * bodyVelocity.sqrMagnitude * cord * span;
		float L = lift_coeff * 0.5f * airDensity * bodyVelocity.sqrMagnitude * cord * span;

        //Convert to body frame
        float Fx = -D * Mathf.Cos(alpha) + L * Mathf.Sin(alpha);
        float Fy = D * Mathf.Sin(alpha) + L * Mathf.Cos(alpha);
        Vector2 force = new Vector2(Fx, Fy);

        print("Body Force: " + force.ToString());

        // Convert to inertial frame
		force = transform.TransformVector(force);

        print("Inertial Force: " + force.ToString());

		return force;
	}
}
