using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
script: platform.cs
event trigger: box collider 2d, is trigger: true, size: event trigger area
physical platform: box collider 2d, default
*/

public class Platform : Entity
{
    private Platform instance;
    public GameObject button;
    public QuestIndicator questIndicator;

    private void Awake() 
    {
        instance = this;
        instance.GetComponent<Renderer>().enabled = false;
        button.SetActive(false); //Button is not visibile.
        button.GetComponent<Button>().onClick.AddListener(TaskOnClick); //Binding On Click Method.
    }

    // Triggers when plane is close to platform
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            instance.GetComponent<Renderer>().enabled = true;
        }
    }

    // Triggers when plane is on platform
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            // cutscene
        } // Need to check if CutScene is done.
        button.SetActive(true); //Shows Button on Screen
    }

    void TaskOnClick(){
        PlatformManager.cutSceneDone = true; //Sets that the CutSceneis Done and Player wants to fly
        Destroy(this.gameObject); //Destorys the Platform
        button.SetActive(false); //Button disappears.
    }


}
