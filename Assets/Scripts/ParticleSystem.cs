using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem : MonoBehaviour
{
    public bool hasClicked;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            hasClicked = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            if (hasClicked == true) {
                StartCoroutine(waitforseconds());
            }
            hasClicked = false;
        }
    }

    IEnumerator waitforseconds() {
        Debug.Log("Here");
        this.GetComponent<Renderer>().enabled = true;
        Debug.Log("WATIING");
        yield return new WaitForSeconds(3);
        Debug.Log("STOPPED");
        hasClicked = false;
        this.GetComponent<Renderer>().enabled = false;
    }
}
