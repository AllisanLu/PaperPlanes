using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;

public class Plane : Entity
{
	public bool invincible = false;
	public GameObject a;

	public PlaneController controller;
	public Animator animator;
	public Aerodynamic aerodynamics;
	private bool onPlatform = false;
	public Shield shield;
	public bool IsActive;
	public int frameCounter = 0;
	public ParticleSystem collisionParticles;
	public ParticleSystem deathParticles;
	public ParticleSystem windTrail;

	private Animator shieldAnim;
	private bool planeDead;
	public static FMOD.Studio.EventInstance Death;

	private float timeToLand = 1.5f;
	private float timePassedLanding = 0f;
	private bool autonomousLanding = false;
	private bool canTriggerLanding = false;
	private Vector3 landingStart;
	private Vector3 landingGoal;
	private float timeAfterLiftOff = 0;
	private float afterLiftOffSafety = 3f;
	private bool tookOff = false;

    // Use this for initialization
    void Start 	() {
		Death = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Death");

		planeDead = false;
		this.gameObject.transform.position = CheckpointManager.planePosition;
		this.gameObject.transform.rotation = CheckpointManager.planeRotation;
		this.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        controller = this.GetComponent<PlaneController>();
		aerodynamics = this.GetComponent<Aerodynamic>();

		rb = GetComponent<Rigidbody2D>();
		rb.inertia = aerodynamics.inertia;
		rb.velocity = new Vector2(4, 1);

		shield = null;

		collisionParticles.GetComponent<Renderer>().enabled = false;
		deathParticles.GetComponent<Renderer>().enabled = false;

		if(invincible)
        {
			gameObject.layer = 3;
        }
	}

	void Update ()
	{
        a.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);

        if (!autonomousLanding && canTriggerLanding && Input.GetKeyDown(KeyCode.A))
        {
            autonomousLanding = true;
            rb.velocity = new Vector2(0, 0);
            landingStart = this.transform.position;
			float totaldist = Vector3.Distance(landingStart, landingGoal);
			timeToLand = (totaldist / 20) * timeToLand;
            gameObject.layer = 3;
        }

        //give a bit of invicibility to the plane after lift off
        if (!invincible && timeAfterLiftOff > afterLiftOffSafety)
        {
            gameObject.layer = 0;
            tookOff = false;
            timeAfterLiftOff = 0;
        }
        else if (!invincible && tookOff)
        {
            timeAfterLiftOff += Time.deltaTime;
        }
    }

	// Called once per frame
	void FixedUpdate() {
		if (onPlatform) {
			if (rb.rotation > 20) {
                rb.rotation -= 3;
            } else if (rb.rotation < -20) {
                rb.rotation += 3;
            }
		}
		if (planeDead) {
			this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f; 
			return;
		}

		if (canTriggerLanding)
		{
			this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        } else if (!onPlatform)
		{
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }

		//player control + physics
		if (!autonomousLanding)
		{
			Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			if (PlatformManager.cutSceneDone)
			{
				onPlatform = false;
				timeAfterLiftOff = 0;
				tookOff = true;

                //Custom Force
                Vector2 force = new Vector2(15, 15.5F);

				// Scale Velocity according to force.
				rb.velocity = new Vector2(20 * Mathf.Abs(Mathf.Cos(force.x)), 20 * Mathf.Abs(Mathf.Sin(force.y)));
				rb.AddForce(force);

				// Get torque from controller
				float pitchCommand = controller.GetAction();
				rb.AddTorque(pitchCommand * aerodynamics.controlStrength * aerodynamics.bodyVelocity.sqrMagnitude);

				// Stability torque
				rb.AddTorque(-aerodynamics.alpha * aerodynamics.stability * aerodynamics.bodyVelocity.sqrMagnitude);

				// Adjusts plane aerodynamic force based off wind contact
				rb.AddForce(windForce);
				PlatformManager.cutSceneDone = false;


			}
			// if the plane is below the screen it dies
			// else if too high push back down
			//skybox
			if (transform.position.y < 0)
			{
				StartCoroutine(ActivateDeathParticlesAndDie());
			}
			else if (transform.position.y > 22)
			{
				transform.position = new Vector2(transform.position.x, 22);
			}
			else if (transform.position.y > 13)
			{
				rb.AddForce(new Vector2(2, -6.5f));

				if (rb.rotation > 30)
				{
					rb.rotation -= 3;
				}
				else if (rb.rotation < -70)
				{
					rb.rotation += 6;
				}

				if (rb.angularVelocity < -40)
				{
					rb.angularVelocity += 3;
				}
			}

			if (transform.position.y > 21)
			{
				ResourceBar.instance.addResource(-0.12f);
			}

			if (!onPlatform)
			{
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

				if (rb.velocity.magnitude > 20)
				{
					rb.velocity = rb.velocity.normalized * 20;
				}
				animator.SetFloat("speed", rb.velocity.magnitude);
			}

			// Brings force back to original aerodynamic force after being affected by the wind
			windForceDecay();
		} else
		{
			//if it needs to autonomously land, take control and lerp
            float x = Mathf.Lerp(landingStart.x, landingGoal.x, timePassedLanding/timeToLand);
			float y = Mathf.Lerp(landingStart.y, landingGoal.y, timePassedLanding/timeToLand);

			this.transform.position = new Vector3(x, y, 0);
			timePassedLanding += Time.deltaTime;
        }
	}

	public void setShield(Shield shield)
	{
		this.shield = shield;
		shieldAnim = shield.gameObject.GetComponent<Animator>();
	}

	// Commits death on the plane and restarts the screen
	public void die() {
		//die and respawn
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void takeDamage(int damage)
    {
		if (!(shield != null && !shield.IsActive()))
		{
			ResourceBar.instance.collision(damage);
		} else
        {
			shield.setIsActive(false);
			shieldAnim.SetBool("dying", true);
		}
	}

	public void setCanLanding(Vector3 platform)
	{
		canTriggerLanding = true;
		landingGoal = platform;
    }

	// Add collider for plane usually collision with obstacles to play death animations
	public void OnCollisionEnter2D(Collision2D other)
	{
		//Check if collision is with Shield object
		//if it is, protect plane once

		if (other.collider.gameObject.CompareTag("Platform"))
		{
			// turn on collision particles
			collisionParticles.GetComponent<Renderer>().enabled = true;
			// call method to remove particles after 1s
			StartCoroutine(RemoveCollisionParticles());

			rb.velocity = new Vector2(0, 0);

            autonomousLanding = false;
			canTriggerLanding = false;
			timePassedLanding = 0f;

			onPlatform = true;
		} else {
			//Object reference not set to an instance of an object
			if (shield != null && shield.IsActive())
			{
				// turn on collision particles
				collisionParticles.GetComponent<Renderer>().enabled = true;
				// call method to remove particles after 1s
				StartCoroutine(RemoveCollisionParticles());
				shield.setIsActive(false);
				shieldAnim.SetBool("dying", true);
				shield = null;
				if (other.collider.gameObject.CompareTag("Water"))
                {
					other.collider.GetComponent<WaterSprout>().die();
                }
			}
			else
			{

				// Check if collision is with an obstacle
				if (other.collider.gameObject.CompareTag("Obstacle"))
				{
					// Call death method to respawn
					// TODO: Add an animation after collision before respawn for
					//       better playability
					Death.start();
					Death.release();
					StartCoroutine(ActivateDeathParticlesAndDie());
				}
				else if (other.collider.gameObject.CompareTag("Water"))
				{
					// Call death method to respawn
					// TODO: Add an animation after collision before respawn for
					//       better playability
					Death.start();
					Death.release();
					StartCoroutine(ActivateDeathParticlesAndDie());
				} 
				else 
				{
					// turn on collision particles
					collisionParticles.GetComponent<Renderer>().enabled = true;
					// call method to remove particles after 1s
					StartCoroutine(RemoveCollisionParticles());
				}
			}
		}
	}

	public IEnumerator RemoveCollisionParticles() {
		while (true) {
			yield return new WaitForSeconds(1);
			collisionParticles.GetComponent<Renderer>().enabled = false;
		}
	}

	public IEnumerator ActivateDeathParticlesAndDie() {
		while(true) {
			planeDead = true;

			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f; 

			collisionParticles.GetComponent<Renderer>().enabled = false;
			windTrail.GetComponent<Renderer>().enabled = false;

			deathParticles.transform.position = this.gameObject.transform.position;

			deathParticles.GetComponent<Renderer>().enabled = true;
			deathParticles.GetComponent<ParticleSystem>().Play();

			yield return new WaitForSeconds(1);

			deathParticles.GetComponent<Renderer>().enabled = false;
			die();
		}
	}
}
