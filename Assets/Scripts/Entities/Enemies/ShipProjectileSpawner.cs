using UnityEngine;
using System;
// using System.Random;
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
    public float fireDelay;
    public float quickFireDelay;
    // public Random rand = new Random();

    private List<int[]> firingLibrary;
    private List<int[]> phase1;
    private List<int[]> phase2;
    
    // private List<int[]> phase3;

    // To track which of the 3 phases we are in in the future
    private int phase = 0;

    // public boolean setRandomFire;

    private Camera cam;

    // This script will simply instantiate the Prefab when the game starts.
    void Start(){
        // Debugging firing pattern
        // Firing pattern mapping: firedObjects[5 items][number of waves]


        phase1 = new List<int[]>() {
            new int[] {0, 0, 0, 2, 1}, 
            new int[] {0, 0, 1, 1, 0},
            new int[] {0, 1, 1, 0, 0},
            new int[] {1, 1, 0, 0, 0},
            new int[] {1, 1, 1, 0, 1},
            new int[] {1, 0, 1, 1, 1},
            new int[] {1, 1, 0, 1, 1},
            new int[] {0, 1, 1, 1, 1},
            new int[] {1, 0, 1, 0, 1},
            new int[] {0, 0, 1, 1, 1},
            new int[] {1, 1, 1, 0, 0},
        };
        phase2 = new List<int[]>() {
            new int[] {0, 0, 1, 0, 0},
            new int[] {1, 1, 0, 1, 1},
            new int[] {},
            new int[] {1, 1, 0, 1, 1},
            new int[] {0, 0, 1, 0, 0},
            new int[] {},
            new int[] {0, 0, 1, 0, 0},
            new int[] {0, 1, 0, 1, 0},
            new int[] {1, 0, 0, 0, 1},
            new int[] {},
            new int[] {1, 0, 0, 0, 1},
            new int[] {0, 1, 0, 1, 0},
            new int[] {0, 0, 1, 0, 0},
            new int[] {},
            new int[] {1, 0, 0, 0, 0},
            new int[] {0, 1, 0, 0, 0},
            new int[] {0, 0, 1, 0, 0},
            new int[] {0, 0, 0, 1, 0}
        };
    }

    public IEnumerator startPhase1() {
        yield return StartCoroutine(startSpawning(phase1, false));
    }

    public IEnumerator startPhase2() {
        yield return StartCoroutine(startSpawning(phase2, true));
    }

    // public IEnumerator startGuassianRandom(int numWaves, int averageNumFired) {
    //     Random rand = new Random(); //reuse this if you are generating many
    //     double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
    //     double u2 = 1.0-rand.NextDouble();
    //     double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
    //                 Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
    //     double randNormal =
    //                 mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
    // }

    // public IEnumerator startSetRandom(int numWaves, int numCannonsFired) {
    //     List<int[]> randomPhase = new List<int[]>();
    //     for (int i = 0; i < numWaves; i++) {
    //         int randNum = rand.Next(0, randomList.Length);
    //         int[] wave = new int[5];
    //         while(wave[randNum] != 0) {
    //             randNum[i] = 1;
    //         }
    //         randomPhase.Add(wave);
    //     }
    //     yield return StartCoroutine(startSpawning(randomPhase, false));
    // }

    private IEnumerator startSpawning(List<int[]> objects, bool usingQuickfire) {
        // Goes through every "wave" of objects and waits fireDelay before going to the next wave
        foreach (int[] wave in objects) {
            spawnItemsFromPattern(Array.ConvertAll(wave, projectile => (firedObjects) projectile));
            if (usingQuickfire && wave.Length != 0) {
                yield return new WaitForSeconds(quickFireDelay);
            } else {
                yield return new WaitForSeconds(fireDelay);
            }
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