using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public bool start;
    public Quest quest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //call the quest and put dialog on screen lol

        //updates the quests the player has
        if (other.gameObject.tag == "Player")
        {
            if (start)
            {
                QuestSystem.AddQuest(quest);
            }
            else
            {
                QuestSystem.RemoveQuest(quest);
            }
        }

    }  
}
