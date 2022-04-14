using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemScript : MonoBehaviour
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
        this.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(2);
        hasClicked = false;
        this.GetComponent<Renderer>().enabled = false;
    }
}
