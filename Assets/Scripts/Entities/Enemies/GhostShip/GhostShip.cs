using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostShip : Entity
{
    private Camera cam;
    private float animationSpeed = 0.01f;
    private ShipProjectileSpawner spawnerScript;
    private SpriteRenderer shipSprite;
    public Sprite bigShip;
    private Component[] a;
    // adjust lines 42, 56, 64 for animations

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        shipSprite = GetComponent<SpriteRenderer>();
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
            // Debug.Log("Entered ghost ship region");

            transform.parent = cam.transform;
            transform.position += new Vector3(5, -15, 3);
            shipSprite.color = new Color (0.7478169f, 0.6681648f, 0.990566f, 1);
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
        // Debug.Log(transform.position.y);
        while (transform.position.y < 10) {
            transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(animationSpeed);
        }
    }

    IEnumerator cutscene() {
        // Debug.Log("Cutscene.");
        yield return null;
    }

    IEnumerator animateToPosition()
    {
        float deltaY = .1f;
        while (transform.position.y < 25) {
            transform.position += new Vector3(0, deltaY, 0);
            deltaY += .005f;
            yield return new WaitForSeconds(animationSpeed);
        }
        transform.position += new Vector3(12f, 10, -1.5f);
        yield return new WaitForSeconds(animationSpeed*40);
        this.GetComponent<SpriteRenderer>().sprite = bigShip;
        shipSprite.sortingOrder = 1;
        shipSprite.color = new Color (1, 1, 1, 1);
        renderShip();

        deltaY = -.5f;
        while (transform.position.y > 11.25) {
            transform.position += new Vector3(0, deltaY, 0);
            deltaY += .0052321f;
            yield return new WaitForSeconds(animationSpeed);
        }

        yield return new WaitForSeconds(animationSpeed*25);

        //startGaussianRandom(numWaves, average, stdDistr)
        //This method starts spawning numWaves of waves with the number of cannons averaging around "average" value
        yield return StartCoroutine(spawnerScript.startPhase1());
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(spawnerScript.startPhase2());
    }

    IEnumerator endCutscene() {
        // Debug.Log("Cutscene.");
        yield return null;
    }

    IEnumerator animateOut()
    {
        while (transform.position.y < 35) {
            transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(animationSpeed);
        }
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("L3");
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
