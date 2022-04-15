using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateQuest : MonoBehaviour
{
    [SerializeField]
    private Text questDisplay;

    //public GameObject questDisplay;
    private Animator anim;
    private bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        Color color = questDisplay.color;
        color.a = 0f;
        questDisplay.color = color;
    
    }

    // Update is called once per frame
    void Update()
    {
        Color color = questDisplay.color;
        if (anim.GetBool("Full")) {
            color.a = 1f;
        }
        if (anim.GetBool("IdleCollapsed")) {
            color.a = 0f;
        }
        questDisplay.color = color;

        if (Input.GetKeyDown(KeyCode.Q)) {
            // toggle visibility of questDisplay
            isOn = !isOn;
            if (isOn) {
                anim.SetTrigger("Unroll");

                StartCoroutine(colorDelay(color, questDisplay));
            }
            else {
                anim.SetTrigger("Reroll");
            }
/*            isOn = !isOn;
            questDisplay.SetActive(isOn);*/
        }
        // Display all current quests in Text UI element
        ArrayList quests = QuestSystem.GetCurrentQuests();
        //questDisplay.GetComponent<Text>().text = "Messages:";
        string text = "";
        foreach (Quest q in quests) {
            // Add quest description per line

            if (q.completed)
            {
                text += q.QuestDescription + " (C) "+  "\n";
            } else
            {
                text += q.QuestDescription + "\n";
            }
/*            GameObject go = new GameObject();
            go.AddComponent<Text>();
            go.GetComponent<Text>().text = q.QuestDescription;
            go.transform.SetParent(questDisplay.transform, false);*/
        }
        // Set UI text to quest description text
        questDisplay.text = text;
    }

    public IEnumerator colorDelay(Color color, Text questDisplay) {
        yield return new WaitForSeconds(0.75f);
        questDisplay.color = color;
    }
}
