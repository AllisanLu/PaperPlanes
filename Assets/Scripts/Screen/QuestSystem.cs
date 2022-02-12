using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    private ArrayList quests;
    private string currentQuest;

    public static QuestSystem instance;

    private void Awake() {
        instance = this;
    } 

    // Start is called before the first frame update
    void Start()
    {
        quests = new ArrayList();
        quests.Add("Test");
        currentQuest = (string) quests[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetCurrentQuest() {
        return currentQuest;
    }

    public void SetCurrentQuest(string quest) {
        quests.Remove(currentQuest);
        currentQuest = quest;
    }

    public void AddQuest(string quest) {
        quests.Add(quest);
    }

    public void RemoveQuest(string quest) {
        quests.Remove(quest);
    }
}
