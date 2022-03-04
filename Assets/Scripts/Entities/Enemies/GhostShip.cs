using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostShip : Entity
{
    private Camera cam;
    private float animationSpeed = 0.01f;
    private ShipProjectileSpawner spawnerScript;
    public Sprite bigShip;
    private Component[] a;
    // adjust lines 42, 56, 64 for animations

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.GetComponent<Renderer>().enabled = false;
        a = this.GetComponentsInChildren(typeof(Renderer));
        foreach (Component b in a)
        {
            Renderer c = (Renderer) b;
            c.enabled = false;
        }
        spawnerScript = GetComponentInChildren<ShipProjectileSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && this.GetComponent<Renderer>().enabled == false)
        {
            Debug.Log("Entered ghost ship region");

            transform.parent = cam.transform;
            transform.position += new Vector3(5, -15, 0);
            this.GetComponent<Renderer>().enabled = true;
            StartCoroutine(startGhostShip());
        }
    }

    IEnumerator startGhostShip()
    {
        yield return StartCoroutine(animateIn());
        yield return StartCoroutine(cutscene());
        yield return StartCoroutine(animateToPosition());
        yield return StartCoroutine(endCutscene());
        yield return StartCoroutine(animateOut());
    }

    IEnumerator animateIn()
    {
        Debug.Log(transform.position.y);
        while (transform.position.y < 10) {
            transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(animationSpeed);
        }
    }

    IEnumerator cutscene() {
        Debug.Log("Cutscene.");
        yield return null;
    }

    IEnumerator animateToPosition()
    {
        while (transform.position.y < 25) {
            transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(animationSpeed);
        }

        transform.position += new Vector3(15f, 10, 0f);
        yield return new WaitForSeconds(animationSpeed*25);
        this.GetComponent<SpriteRenderer>().sprite = bigShip;
        renderShip();
        while (transform.position.y > 11.25) {
            transform.position += new Vector3(0, -0.1f, 0);
            yield return new WaitForSeconds(animationSpeed);
        }

        yield return new WaitForSeconds(animationSpeed*25);

        yield return StartCoroutine(spawnerScript.startPhase1());
        yield return StartCoroutine(spawnerScript.startPhase2());
        yield return new WaitForSeconds(3);
    }

    IEnumerator endCutscene() {
        Debug.Log("Cutscene.");
        yield return null;
    }

    IEnumerator animateOut()
    {
        Debug.Log(transform.position.y);
        while (transform.position.y < 35) {
            transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(animationSpeed);
        }
        SceneManager.LoadScene("GameEnd");
        CheckpointManager.resetPosition();
    }

    private void renderShip() {
        this.GetComponent<Renderer>().enabled = true;
        foreach (Component b in a)
        {
            Renderer c = (Renderer) b;
            c.enabled = true;
        }
    }
}
