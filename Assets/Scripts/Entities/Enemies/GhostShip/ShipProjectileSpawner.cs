using UnityEngine;
using System;
using Random=System.Random;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    public Random rand = new Random();

    private List<int[]> firingLibrary;
    private List<int[]> phase1;
    private List<int[]> phase2;
    
    // private List<int[]> phase3;

    // To track which of the 3 phases we are in in the future

    // public boolean setRandomFire;

    private Camera cam;

    // This script will simply instantiate the Prefab when the game starts.
    void Start(){
        // Debugging firing pattern
        // Firing pattern mapping: firedObjects[5 items][number of waves]


        phase1 = new List<int[]>() {
            new int[] {0, 2, 0, 2, 1}, 
            new int[] {0, 2, 1, 1, 0},
            new int[] {2, 1, 1, 0, 0},
            new int[] {1, 1, 0, 0, 2},
            new int[] {1, 1, 1, 0, 1},
            new int[] {1, 0, 1, 1, 1},
            new int[] {1, 1, 0, 1, 1},
            new int[] {0, 1, 1, 1, 1},
            new int[] {1, 0, 1, 0, 1},
            new int[] {0, 0, 1, 1, 1},
            new int[] {1, 1, 1, 0, 0},
        };
        phase2 = new List<int[]>() {
            new int[] {2, 2, 1, 2, 2},
            new int[] {1, 1, 2, 1, 1},
            new int[] {},
            new int[] {1, 1, 2, 1, 1},
            new int[] {2, 2, 1, 2, 2},
            new int[] {},
            new int[] {2, 2, 1, 2, 2},
            new int[] {2, 1, 2, 1, 2},
            new int[] {1, 2, 2, 2, 1},
            new int[] {},
            new int[] {1, 2, 2, 2, 1},
            new int[] {2, 1, 2, 1, 2},
            new int[] {2, 2, 1, 2, 2},
            new int[] {},
            new int[] {1, 2, 2, 2, 2},
            new int[] {2, 1, 2, 2, 2},
            new int[] {2, 2, 1, 2, 2},
            new int[] {2, 2, 2, 1, 2},
            new int[] {2, 2, 2, 2, 1}
        };
    }

    public IEnumerator startPhase1() {
        yield return StartCoroutine(startSpawning(phase1, false));
    }

    public IEnumerator startPhase2() {
        yield return StartCoroutine(startSpawning(phase2, true));
    }
    
    /// Spawns cannon balls where the number of cannons fired per wave is centered on averageNumFired with a standard distribution and number of waves
    public IEnumerator startGuassianRandom(int numWaves, int averageNumFired, double stdDev) {
        for (int i = 0; i < numWaves; i++) {
            int randomNumSpawned = (int) Math.Round(calcGaussianValue(averageNumFired, stdDev));
            if (randomNumSpawned > 5) randomNumSpawned = 5;
            if (randomNumSpawned < 0) randomNumSpawned = 0;
            int[] firingSequence = populateFiringArray(randomNumSpawned);
            spawnItemsFromPattern(firingSequence);
            yield return new WaitForSeconds(fireDelay);
        }
    }

    ///Calculates a value based on a Gaussian distrbution centered around an average with a specificed standard distribution
    private double calcGaussianValue(int averageNumFired, double stdDev) {
        double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0-rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                    Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        double randNormal =
                    averageNumFired + stdDev * randStdNormal; //random normal(mean,stdDev^2)
        return randNormal;
    }

    ///Helper method that populates a with numOfObjects number of cannon balls which is calculated from the Gaussian distribution
    private int[] populateFiringArray(int numOfObjects) {
        int[] firingSequence = new int[5];
        List<int> possible = Enumerable.Range(0, 5).ToList();
        for (int i = 0; i < numOfObjects; i++) {
            int index = rand.Next(0, possible.Count());
            firingSequence[index] = 1;
            possible.RemoveAt(index);
        }
        return firingSequence;
    }

    private IEnumerator startSpawning(List<int[]> objects, bool usingQuickfire) {
        // Goes through every "wave" of objects and waits fireDelay before going to the next wave
        foreach (int[] wave in objects) {
            spawnItemsFromPattern(wave);
            if (usingQuickfire && wave.Length != 0) {
                yield return new WaitForSeconds(quickFireDelay);
            } else {
                yield return new WaitForSeconds(fireDelay);
            }
        }
    }

    void spawnItemsFromPattern(int[] spawningArray) {
        // Spawns every item in a wave at 5 different locations on the screen
        firedObjects[] wave = Array.ConvertAll(spawningArray, projectile => (firedObjects) projectile);
        for (int i = 0; i < wave.Length; i++) {
            switch (wave[i]) {
                case firedObjects.CANNONBALL:
                    StartCoroutine(cannonSpawns[i].GetComponent<Cannon>().fireProjectile());
                    break;
                case firedObjects.JAVLIN: 
                    StartCoroutine(javelinSpawns[i].GetComponent<Crossbow>().fireProjectile());
                    break;
            }
        }
    }


}