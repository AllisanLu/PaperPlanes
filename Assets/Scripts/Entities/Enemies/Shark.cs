using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Enemy
{
    public float expectedDistance = 5;
    public float expectedHeight = 5;
    public float speed;

    private Vector2 startVelocity;
    private Vector2 startPos;
    private Vector2 targetPos;

    private float dist;
    private float nextX;
    private float baseY;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        behaviorController = GetComponent<SharkController>();
        startVelocity = ((SharkController)behaviorController).getStartVelocity(expectedDistance, expectedHeight);
        startPos = transform.position;
        targetPos = new Vector2(startPos.x + expectedDistance, 0);
        rb.gravityScale = 0;
    }

    public override void Move()
    {
        dist = targetPos.x - startPos.x;
        nextX = Mathf.MoveTowards(transform.position.x, targetPos.x, speed * Time.deltaTime);
        baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - startPos.x) /dist);
        height = expectedHeight * (nextX - startPos.x) * (nextX - targetPos.x) / (-0.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;

        if ((Vector2) transform.position == targetPos)
        {
            transform.position = startPos;
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

    void OnDrawGizmos()
    {
        //Draw the parabola by sample a few times
        Gizmos.color = Color.red;
        Vector2 endPos = new Vector2(transform.position.x, transform.position.y) + new Vector2(expectedDistance, 0);
        Gizmos.DrawLine(transform.position, endPos);
        float count = 20;
        Vector2 lastP = startPos;
        for (float i = 0; i < count + 1; i++)
        {
            Vector3 p = SampleParabola(transform.position, endPos, expectedHeight, i / count);
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
            Vector3 levelDirecteion = end - new Vector3(start.x, end.y, start.z);
            Vector3 right = Vector3.Cross(travelDirection, levelDirecteion);
            Vector3 up = Vector3.Cross(right, travelDirection);
            if (end.y > start.y) up = -up;
            Vector3 result = start + t * travelDirection;
            result += ((-parabolicT * parabolicT + 1) * height) * up.normalized;
            return result;
        }
    }
    #endregion
}
