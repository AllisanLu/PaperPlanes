using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            Vector3 newPos = new Vector3(this.gameObject.transform.position.x, 15f, 0);
            CheckpointManager.planePosition = newPos;
            Debug.Log("CHECKPOINT!");
        }
    }
}
