using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColumnSpawner : MonoBehaviour
{
    public float maxTime = 3;
    private float timer = 0;
    public GameObject columns;
    public float height;
    public float width;

    // Start is called before the first frame update
    void Start()
    {
        GameObject col = Instantiate(columns);
        col.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
    }

    // Update is called once per frame
    void Update()
    {
        ResourceBar.instance.setRegenerationSpeed(ResourceBar.instance.getCapacity() - ResourceBar.instance.getCurrentResources());
        if (timer > maxTime)
        {
            this.transform.position = this.transform.position + new Vector3(80, 0, 0);
            GameObject col = Instantiate(columns);
            col.transform.position = this.transform.position + new Vector3(0, Random.Range(-height, height), 0);
            Destroy(col, 30);
            timer = 0;
        }
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject transitionObject = LevelTransitions.instance.gameObject;
            LevelTransitions transitionScript = transitionObject.GetComponent<LevelTransitions>();
            StartCoroutine(transitionScript.LevelTransition("L1"));
        }
    }
}
