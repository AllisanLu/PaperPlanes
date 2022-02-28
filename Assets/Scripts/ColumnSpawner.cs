using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (timer > maxTime)
        {
            this.transform.position = this.transform.position + new Vector3(70, 0, 0);
            GameObject col = Instantiate(columns);
            col.transform.position = this.transform.position + new Vector3(0, Random.Range(-height, height), 0);
            Destroy(col, 10);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
