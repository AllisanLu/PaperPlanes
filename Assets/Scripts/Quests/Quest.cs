using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string QuestDescription;
    public bool current;
    public bool completed;
    public ArrayList dialogue;

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

    public override bool Equals(object? obj)
    {
        if (obj.GetType().Name == "Quest")
        {
            return ((Quest)obj).QuestDescription == this.QuestDescription;
        }
        return false;
    }

}
