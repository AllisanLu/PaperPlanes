using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Enemy
{
    public float runDistance;
    public float runSpeed;
    public int startoffset;
    public int endoffset;

    public float jumpHeight;
    public float jumpDistance;
    public float jumpSpeed;

    private Vector2 jumpVelocity;
    private Vector2 jumpTarget;
    private Vector2 leftTarget;
    private Vector2 rightTarget;
    private Vector2 jumpStart;

    private float dist;
    private float nextX;
    private float baseY;
    private float height;
    private float runX;

    private bool reverse = false;
    private bool barked = false;
    public static FMOD.Studio.EventInstance Bark;

    private Plane plane;

   // private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Bark = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Dog");

        rb = GetComponent<Rigidbody2D>();   
        behaviorController = GetComponent<DogController>();
        plane = FindObjectOfType<Plane>();

        jumpVelocity = ((DogController)behaviorController).getStartVelocity(jumpDistance, jumpHeight);

        leftTarget = new Vector2(transform.position.x, 0); // run start
        jumpStart = new Vector2(transform.position.x + runDistance, 0);
        rightTarget = new Vector2(jumpStart.x + jumpDistance * 2, 0);

        rb.gravityScale = 0;
    }

    void Update()
    {
        if(GetComponent<Renderer>().isVisible && !barked)
        {   
            barked = true;
            Bark.start();

        }
    }
    public override void Move()
    {   
        if (plane.gameObject.transform.position.x + startoffset > transform.position.x && plane.gameObject.transform.position.x < transform.position.x + endoffset) {
            if (transform.position.x >= jumpStart.x) {
                animator.SetBool("Run", true);
                if (reverse) {
                    if (transform.localScale.x < 0)
                    {
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    }
                    Run(-1);
                } else {
                    animator.SetBool("Jump", true);
                    Jump(jumpStart, rightTarget);
                    animator.SetBool("Jump", false);
                }
            } else {
                animator.SetBool("Run", true);
                if (reverse) {
                    animator.SetBool("Jump", true);
                    Jump(jumpStart, leftTarget);
                    animator.SetBool("Jump", false);
                } else {
                    if (transform.localScale.x > 0)
                    {
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    }
                    Run(1);
                }
            }
        }
    }

    public void Run(int direction) {
        //runX = Mathf.Lerp(transform.position.x, startPos.x, 0.5f * Time.deltaTime);
        Vector3 runPosition = new Vector3(transform.position.x + (runSpeed * direction), 0, transform.position.z);
        transform.position = runPosition;
    }

    public void Jump(Vector2 start, Vector2 target) {
        dist = target.x - start.x;
        nextX = Mathf.MoveTowards(transform.position.x, target.x, jumpSpeed * Time.deltaTime);
        baseY = Mathf.Lerp(start.y, target.y, (nextX - start.x) /dist);
        height = jumpHeight * (nextX - start.x) * (nextX - target.x) / (-0.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        //transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;

        if ((Vector2) transform.position == target) {
            reverse = !reverse;
        }
    }


    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(180, 0, -Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }


    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.collider.GetComponent<Plane>().takeDamage(damage);
        }

    }
    

    /*void OnDrawGizmos()
    {
        //Draw the parabola by sample a few times
        Gizmos.color = Color.red;
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y) + new Vector2(jumpDistance, 0);
        Gizmos.DrawLine(transform.position, rightTarget);
        float count = 20;
        Vector2 lastP = jumpStart;
        for (float i = 0; i < count + 1; i++)
        {
            Vector3 p = SampleParabola(transform.position, rightTarget, jumpHeight, i / count);
            Gizmos.color = i % 2 == 0 ? Color.blue : Color.green;
            Gizmos.DrawLine(lastP, p);
            lastP = p;
        }
    }

    #region Parabola sampling function
    /// <summary>
    /// Get position from a parabola defined by start and end, height, and time
    /// </summary>
    /// <param name='start'>
    /// The start point of the parabola
    /// </param>
    /// <param name='end'>
    /// The end point of the parabola
    /// </param>
    /// <param name='height'>
    /// The height of the parabola at its maximum
    /// </param>
    /// <param name='t'>
    /// Normalized time (0->1)
    /// </param>S
    Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolicT = t * 2 - 1;
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector3 travelDirection = end - start;
            Vector3 result = start + t * travelDirection;
            result.y += (-parabolicT * parabolicT + 1) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector3 travelDirection = end - start;
            Vector3 levelDirection = end - new Vector3(start.x, end.y, start.z);
            Vector3 right = Vector3.Cross(travelDirection, levelDirection);
            Vector3 up = Vector3.Cross(right, travelDirection);
            if (end.y > start.y) up = -up;
            Vector3 result = start + t * travelDirection;
            result += ((-parabolicT * parabolicT + 1) * height) * up.normalized;
            return result;
        }
    }
    #endregion*/
}
