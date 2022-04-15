using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/********************
 * DIALOGUE MANAGER *
 ********************
 * This Dialogue Manager is what links your dialogue which is sent by the Dialogue Trigger to Unity
 *
 * The Dialogue Manager navigates the sent text and prints it to text objects in the canvas and will toggle
 * the Dialogue Box when appropriate
 */

public class StoryManager : MonoBehaviour
{
    public GameObject CanvasBox; // your fancy canvas box that holds your text objects
    public Text TextBox; // the text body
    public bool freezePlayerOnDialogue = true;

    // private bool isOpen; // represents if the dialogue box is open or closed

    private Queue<string> inputStream = new Queue<string>(); // stores dialogue
    private PlaneController animController;
    int UILayer;

    private void Start()
    {
        CanvasBox.SetActive(false); // close the dialogue box on play
        animController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlaneController>();

        // this is used to detect if mouse if over ui elements
        // if you want this to specifically detect only with dialogue related stuff, consider using different layer name
        // but this technically works with the center stuff
        UILayer = LayerMask.NameToLayer("UI");
    }

    private void DisablePlayerController()
    {
        var rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        // plane controller doesn't have a ForceIdle() method, it seems liek it's supposed to "freeze" the player?
        // animController.ForceIdle();

        // if u want, just implement or call a freeze method here
        // im not sure how the owners want freezing to be implemented or whether or not they want freezing or not
        // so im just leaving this blank

        animController.enabled = false;
    }

    private void EnablePlayerController()
    {
        animController.enabled = true;
    }

    public void StartDialogue(Queue<string> dialogue)
    {
        if (freezePlayerOnDialogue)
        {
            Time.timeScale = 0f;
        }

        CanvasBox.SetActive(true); // open the dialogue box
        // isOpen = true;
        inputStream = dialogue; // store the dialogue from dialogue trigger
        PrintDialogue(); // Prints out the first line of dialogue
    }

    public void AdvanceDialogue() // call when a player presses a button in Dialogue Trigger
    {
        PrintDialogue();
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdvanceDialogue();
        }
        Debug.Log("this ran pagchomp");
    }


    private void PrintDialogue()
    {
        if (inputStream.Count == 0 || inputStream.Peek().Contains("EndQueue")) // special phrase to stop dialogue
        {
            if (inputStream.Count > 0)
            {
                inputStream.Dequeue(); // Clear Queue
            }
            EndDialogue();
        }
        else if (inputStream.Peek().Contains("[NAME="))
        {
            string name = inputStream.Peek();
            name = inputStream.Dequeue().Substring(name.IndexOf('=') + 1, name.IndexOf(']') - (name.IndexOf('=') + 1));
           // NameText.text = name;
            // for the portrait, havent implemented, just an idea, we can just search for a gameobject or image file with the same name and display it in a box (i think)
            // use this for the circle mask, https://subscription.packtpub.com/book/game-development/9781785885822/1/ch01lvl1sec13/adding-a-circular-mask-to-an-image
            // im too lazy to pull out photoshop atm

            // use dictionary to store the names and sprites, make sure name matches with names in the script
            // then, do portrait = dictionary_name[name]

            // Sprite portrait = 
            // Portrait.sprite = portrait;

            PrintDialogue(); // print the rest of this line
        }
        else
        {
            TextBox.text = inputStream.Dequeue();
        }
    }

    public void EndDialogue()
    {
        TextBox.text = "";
      //  NameText.text = "";
        inputStream.Clear();
        CanvasBox.SetActive(false);
        // isOpen = false;
        if (freezePlayerOnDialogue)
        {
            Time.timeScale = 1f;
        }
    }
    // yoink, https://forum.unity.com/threads/how-to-detect-if-mouse-is-over-ui.1025533/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AdvanceDialogue();
        }
        //print(IsPointerOverUIElement() ? "Over UI" : "Not over UI");
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

}