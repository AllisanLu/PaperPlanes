using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckpointManager
{
    // static position and rotation variables which the plane restarts at 
    // every time we load the level scene

    // planePosition should change every time we hit a new checkpoint
    public static Vector3 planePosition = new Vector3(-6.6f, 15f, 0f);
    public static Quaternion planeRotation = Quaternion.Euler(0f, 1.127f, -21.583f);

    public static void resetPosition()
    {
        planePosition = new Vector3(-6.6f, 15f, 0f);
        planeRotation = Quaternion.Euler(0f, 1.127f, -21.583f);
    }
}
