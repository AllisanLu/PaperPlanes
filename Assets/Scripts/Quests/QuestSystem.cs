using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    private static ArrayList quests = new ArrayList();

    public static QuestSystem instance;


    private void Awake() {
        instance = this;

    } 

    // Start is called before the first frame update
    void Start()
    {
      	DontDestroyOnLoad(this);
        AddQuest(new Quest("Lost Message", true, false, null));

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
        if (quests.IndexOf(quest) < 0)
        {
            quests.Add(quest);
        }
    }

    public static Quest getQuest(Quest quest)
    {
        return (Quest) quests[quests.IndexOf(quest)];
    }

    public static bool contains(Quest quest)
    {
        return quests.IndexOf(quest) >= 0;
    }

    public static void RemoveQuest(Quest quest) {
        quests.Remove(quest);
    }
}
