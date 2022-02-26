using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    public GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        GameObject currentSpawner = Instantiate(spawner, cam.transform.position, Quaternion.identity, transform) as GameObject;
        ShipProjectileSpawner spawnerScript = currentSpawner.GetComponent<ShipProjectileSpawner>();
        spawnerScript.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
