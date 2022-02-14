using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string QuestDescription;
    public bool current;
    public bool completed;
    public ArrayList dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Quest(string QuestDescription, bool current, bool completed, ArrayList dialogue) {
        this.QuestDescription = QuestDescription;
        this.current = current;
        this.completed = completed;
        this.dialogue = dialogue;
    }

    public void UpdateQuestDescription(string newQuestDescription) {
        this.QuestDescription = newQuestDescription;
    }

    public void UpdateDialogue(ArrayList dialogue) {
        this.dialogue = dialogue;
    }

    public void UpdateDialogueAtLocation(string dialogue, int index) {
        this.dialogue.RemoveAt(index);
        this.dialogue.Insert(index, dialogue);
    }


    public void UpdateCompleted(bool newCompleted) {
        this.completed = newCompleted;
    }

    public void UpdateCurrent(bool newCurrent) {
        this.current = newCurrent;
    }


}
