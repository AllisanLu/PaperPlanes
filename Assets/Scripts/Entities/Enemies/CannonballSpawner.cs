using UnityEngine;
public class CannonballSpawner : MonoBehaviour 
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject cannonball;
    private Camera cam;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        transform.position = cam.transform.position + new Vector3(25, 0, 0);
        Instantiate(cannonball, transform.position, Quaternion.identity);
    }
}