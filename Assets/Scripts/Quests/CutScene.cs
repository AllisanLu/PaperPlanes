using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public bool start;
    public Quest quest;
    public DialogueTrigger npc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dialog()
    {
        //call the quest and put dialog on screen lol

        npc.TriggerDialogue();

        //set cutscene done to true )b
        if (start)
        {
            QuestSystem.AddQuest(quest);
        } else
        {
            QuestSystem.RemoveQuest(quest);
        }

    }  
}
