using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
script: platform.cs
event trigger: box collider 2d, is trigger: true, size: event trigger area
physical platform: box collider 2d, default
*/

public class Platform : MonoBehaviour
{
    public GameObject button;
    public QuestIndicator questIndicator;
    public double finalYPosition;
    public float fadeSpeed = 1f;

    private void Awake() 
    {
        this.GetComponent<Renderer>().enabled = false;
        button.SetActive(false); //Button is not visibile.
        button.GetComponent<Button>().onClick.AddListener(TaskOnClick); //Binding On Click Method.
    }

    // Start is called before the first frame update
    void Start()
    {
        finalYPosition = transform.position.y;
        transform.position = new Vector2(transform.position.x, transform.position.y - 3);
    }

    // Update is called once per frame
    //If plane is in range of platform, move up and fade in until it hits the final position
    //resets each time plane restarts at checkpoint
    void Update()
    {
        bool inRange = this.GetComponent<Renderer>().enabled;
        
        if (inRange && transform.position.y < finalYPosition) {
            transform.Translate((Vector2.up * (Time.deltaTime * 5)));
            StartCoroutine(FadeInIObject());
        } 
    }

    // Triggers when plane is near the
     void OnTriggerEnter2D(Collider2D other)
     {
        if (other.gameObject.CompareTag("Player"))
         {
            this.GetComponent<Renderer>().enabled = true;
        }
     }

    // Triggers when plane is on platform
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            // cutscene
        } // Need to check if CutScene is done.
        button.SetActive(true); //Shows Button on Screen
    }

    void TaskOnClick(){
        PlatformManager.cutSceneDone = true; //Sets that the CutSceneis Done and Player wants to fly
        Destroy(this.gameObject); //Destorys the Platform
        button.SetActive(false); //Button disappears.
    }
    
    //slowly fades object in
    //first sets transparency to 0
    //while object is not fully transparent, gradually change transparency
    public IEnumerator FadeInIObject()
    {
        Color objColor = this.GetComponent<Renderer>().material.color;
        objColor.a = 0;

        while (objColor.a < 1) { 
            float fadeAmount = objColor.a + (fadeSpeed * Time.deltaTime * 100);
            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objColor;
            yield return null;
        }

    }

}
