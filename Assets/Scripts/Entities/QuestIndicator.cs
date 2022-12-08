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
        DialogueTrigger dialogueTrigger = this.transform.parent.gameObject.GetComponentInChildren<DialogueTrigger>();
        CutScene cutscene = dialogueTrigger.GetComponent<CutScene>();

        if (other.gameObject.CompareTag("Player")  
            && cutscene.start || QuestSystem.contains(cutscene.quest))
        {

            instance.GetComponent<Renderer>().enabled = true;
          
        }
    }

    public void DisableRender()
    {
        instance.GetComponent<Renderer>().enabled = false;
    }
}
