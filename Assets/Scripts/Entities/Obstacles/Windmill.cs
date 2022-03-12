using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : Obstacle
{
    public float baseRotationVal;
    public float rotationDecreaseSpeed;

    private float rotationVal;
    
    void Start()
    {
        rotationVal = baseRotationVal;
    }
    
    void FixedUpdate ()
    {
        transform.Rotate (0, 0, rotationVal * Time.deltaTime);
        Debug.Log(rotationVal);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        WindCurrent wind = other.GetComponent<WindCurrent>();
        if (wind != null)
        {
            float windStrength = wind.getForce();
            if (windStrength > 5 && windStrength < 10) {
                rotationVal += 80;
                StartCoroutine(ReturnToBaseRotationValue());
            } else if (windStrength > 10 && windStrength < 15) {
                rotationVal += 100;
                StartCoroutine(ReturnToBaseRotationValue());
            } else if (windStrength > 15) {
                rotationVal += 120;
                StartCoroutine(ReturnToBaseRotationValue());
            } else {
                rotationVal += 60;
                StartCoroutine(ReturnToBaseRotationValue());
            }
        } 
    }

    IEnumerator ReturnToBaseRotationValue()
    {
        while (rotationVal >= baseRotationVal) {
            rotationVal -= rotationDecreaseSpeed;
            yield return new WaitForSeconds(1f);
        }
        rotationVal = baseRotationVal;
    }
}
