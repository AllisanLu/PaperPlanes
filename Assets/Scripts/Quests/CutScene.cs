using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public bool start;
    public string questName;
    public Quest quest;

    // Start is called before the first frame update
    void Start()
    {
        quest = new Quest(questName, true, false, null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleQuest()
    {
        //call the quest and put dialog on screen lol

        //updates the quests the player 
        if (start)
        {
            QuestSystem.AddQuest(quest);
            //print("added quest");
        }
        else
        {
            Quest complete = QuestSystem.getQuest(quest);
            complete.UpdateCompleted(true);
            //  complete.UpdateCurrent(false);
        }


    }
}
