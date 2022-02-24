using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquallUI : MonoBehaviour 
{ 
    private static int squall;
    private static bool squallActive;
    private static int squallItems;

    public Animator animator = null;

    public void useSquall()
    {
        if (squall > 0)
        {
            squall -= 1;
            animator.SetBool("decrease", true);
            animator.SetInteger("squall", squall);
        }
    }

    //sets squall capacity
    public void setSquallActive()
    {
        squallActive = true;
        squall = 3;
        animator.SetBool("decrease", false);
        animator.SetInteger("squall", squall);
    }

    public bool getSquallActive()
    {
        if (squall == 0)
        {
            squallActive = false;
        }
        return squallActive;
    }
}
