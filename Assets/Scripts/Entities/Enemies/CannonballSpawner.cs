using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonballSpawner : MonoBehaviour 
{
    // Create enum for spawning different objects
    enum firedObjects {NOTHING = 0, CANNONBALL, JAVLIN};

    public GameObject cannonball;
    public int fireDelay;
    public int[][] firePattern;

    // public boolean setRandomFire;

    private Camera cam;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Debugging firing pattern
        // Firing pattern mapping: firedObjects[5 items][number of waves]
        firedObjects[] wave1 = {firedObjects.CANNONBALL, firedObjects.NOTHING, firedObjects.NOTHING, firedObjects.CANNONBALL, firedObjects.CANNONBALL};
        firedObjects[] wave2 = {firedObjects.JAVLIN, firedObjects.JAVLIN, firedObjects.CANNONBALL, firedObjects.CANNONBALL, firedObjects.CANNONBALL};
        List<firedObjects[]> objects = new List<firedObjects[]>() {wave1, wave2};

        StartCoroutine(startSpawning(objects));

    }

    IEnumerator startSpawning(List<firedObjects[]> objects) {
        // Goes through every "wave" of objects and waits fireDelay before going to the next wave
        foreach (firedObjects[] wave in objects) {
            spawnItemsFromPattern(wave);
            yield return new WaitForSeconds(fireDelay);
        }
    }

    void spawnItemsFromPattern(firedObjects[] spawningArray) {

        // Spawns every item in a wave at 5 different locations on the screen
        for (int i = 0; i < spawningArray.Length; i++) {
            Vector3 spawnLocation = cam.transform.position + new Vector3(25, 8 + (-4 * i), 0);
            switch (spawningArray[i]) {
                case firedObjects.CANNONBALL:
                    Instantiate(cannonball, spawnLocation, Quaternion.identity, transform);
                    break;
                case firedObjects.JAVLIN: 
                    Debug.Log("Threw a javlin");
                    break;
            }
        }
    }


}