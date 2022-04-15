using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : Obstacle
{
    public float baseRotationVal;
    public static float rotationDecreaseSpeed;

    private static float rotationVal;
    private Animator anim;

    
    void Start()
    {
        rotationVal = baseRotationVal;
       // anim = this.gameObject.GetComponent<Animator>();
        //GameObject body = transform.parent.gameObject.transform.GetChild(0).gameObject;
        anim = this.transform.parent.gameObject.GetComponent<Animator>();


    }
    
    void FixedUpdate ()
    {
        transform.Rotate (0, 0, rotationVal * Time.deltaTime);
        // Debug.Log(rotationVal);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        WindCurrent wind = other.GetComponent<WindCurrent>();
        if (wind != null)
        {
            float windStrength = wind.getForce();
            if (windStrength > 5 && windStrength < 10) {
               // rotationVal += 80;
                rotationVal += 40;

                anim.SetBool("Slow", true);

                StartCoroutine(ReturnToBaseRotationValue());
            } else if (windStrength > 10 && windStrength < 15) {
               // rotationVal += 100;
                rotationVal += 50;

                anim.SetBool("Slow", false);

                StartCoroutine(ReturnToBaseRotationValue());
            } else if (windStrength > 15) {
               // rotationVal += 120;
                rotationVal += 60;

                anim.SetBool("Slow", false);

                StartCoroutine(ReturnToBaseRotationValue());
            } else {
                //rotationVal += 60;
                rotationVal += 30;

                anim.SetBool("Slow", true);

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
