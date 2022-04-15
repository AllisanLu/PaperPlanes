using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIndicator : MonoBehaviour
{
    public QuestIndicator instance;
    private Quest quest;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        instance.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Shows when plane is within range
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          
           instance.GetComponent<Renderer>().enabled = true;
          
        }
    }

    public void DisableRender()
    {
        instance.GetComponent<Renderer>().enabled = false;
    }
}
