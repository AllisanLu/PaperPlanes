using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateQuest : MonoBehaviour
{
    [SerializeField]
    private Text questDisplay;
    
    private bool isOn = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            // toggle visibility of questDisplay
            Color color = questDisplay.color;
            if (isOn) {
                color.a = 0f;
            } else {
                color.a = 1f;
            }
            isOn = !isOn;
            questDisplay.color = color;
        }
        // Display all current quests in Text UI element
        ArrayList quests = QuestSystem.GetCurrentQuests();
        string text = "CURRENT QUESTS: \n";
        foreach (Quest q in quests) {
            // Add quest description per line
            text += q.QuestDescription + "\n";
        }
        // Set UI text to quest description text
        questDisplay.text = text;
    }
}
