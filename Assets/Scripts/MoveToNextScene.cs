using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    public string nameOfNextScene = "";
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            CheckpointManager.resetPosition();
            GameObject transitionObject = LevelTransitions.instance.gameObject;
            LevelTransitions transitionScript = transitionObject.GetComponent<LevelTransitions>();
            StartCoroutine(transitionScript.LevelTransition(nameOfNextScene));
        }
    }
}
