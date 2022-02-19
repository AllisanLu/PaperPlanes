using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    private static ArrayList quests;

    public static QuestSystem instance;

    private void Awake() {
        instance = this;
    } 

    // Start is called before the first frame update
    void Start()
    {
        quests = new ArrayList();
        // Temporary quests for debugging
        quests.Add(new Quest("Objective 1", true, false, new ArrayList()));
        quests.Add(new Quest("Objective 2", false, false, new ArrayList()));
        quests.Add(new Quest("Objective 3", true, false, new ArrayList()));
        quests.Add(new Quest("Objective 4", true, false, new ArrayList()));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static ArrayList GetCurrentQuests() {
        // Loop through quests to find current quests
        ArrayList currents = new ArrayList();
        foreach (Quest q in quests) {
            if (q.current) {
                currents.Add(q);
            }
        }
        return currents;
    }

    public static void SetCurrentQuest(Quest quest) {
        quest.UpdateCurrent(true);
    }

    public static void AddQuest(Quest quest) {
        quests.Add(quest);
    }

    public static void RemoveQuest(Quest quest) {
        quests.Remove(quest);
    }
}
