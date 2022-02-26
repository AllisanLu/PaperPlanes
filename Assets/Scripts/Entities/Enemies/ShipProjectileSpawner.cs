using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ShipProjectileSpawner : MonoBehaviour 
{
    // Create enum for spawning different objects
    public enum firedObjects {NOTHING = 0, CANNONBALL, JAVLIN};

    public GameObject cannonball;
    public GameObject javlin;
    public GameObject[] cannonSpawns;
    public GameObject[] javelinSpawns;
    public int fireDelay;
    public List<firedObjects[]> firePattern;

    // To track which of the 3 phases we are in in the future
    private int phase = 0;

    // public boolean setRandomFire;

    private Camera cam;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        // Debugging firing pattern
        // Firing pattern mapping: firedObjects[5 items][number of waves]
        firedObjects[] wave1 = {0, 0, 0, (firedObjects) 1, (firedObjects) 1};
        firedObjects[] wave2 = {(firedObjects) 2, (firedObjects) 2, (firedObjects) 1, (firedObjects) 1, (firedObjects) 1};
        firedObjects[] wave3 = {0, 0, 0, (firedObjects) 2, (firedObjects) 2};
        firedObjects[] wave4 = {0, 0, 0, (firedObjects) 1, (firedObjects) 2};
        firePattern = new List<firedObjects[]>() {wave1, wave2, wave3, wave4};
        
        // TODO remove StartCoroutine
        // StartCoroutine(startSpawning(firePattern));
    }

    // Look at SpawnerTest to how to instantiate ShipProjectileSpawner
    public ShipProjectileSpawner() {
        firedObjects[] wave1 = {0, 0, 0, (firedObjects) 1, (firedObjects) 1};
        firedObjects[] wave2 = {(firedObjects) 2, (firedObjects) 2, (firedObjects) 1, (firedObjects) 1, (firedObjects) 1};
        firedObjects[] wave3 = {0, 0, 0, (firedObjects) 2, (firedObjects) 2};
        firedObjects[] wave4 = {0, 0, 0, (firedObjects) 1, (firedObjects) 2};
        firePattern = new List<firedObjects[]>() {wave1, wave2, wave3, wave4};
    }

    public void start() {
        StartCoroutine(startSpawning(firePattern));
    }

    private IEnumerator startSpawning(List<firedObjects[]> objects) {
        // Goes through every "wave" of objects and waits fireDelay before going to the next wave
        foreach (firedObjects[] wave in objects) {
            spawnItemsFromPattern(wave);
            yield return new WaitForSeconds(fireDelay);
        }
    }

    void spawnItemsFromPattern(firedObjects[] spawningArray) {

        // Spawns every item in a wave at 5 different locations on the screen
        for (int i = 0; i < spawningArray.Length; i++) {
            switch (spawningArray[i]) {
                case firedObjects.CANNONBALL:
                    Instantiate(cannonball, cannonSpawns[i].transform.position, Quaternion.identity, transform);
                    break;
                case firedObjects.JAVLIN: 
                    Instantiate(javlin, javelinSpawns[i].transform.position, Quaternion.identity, transform);
                    break;
            }
        }
    }


}