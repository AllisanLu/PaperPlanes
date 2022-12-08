using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> dialogs;
    public GameObject plane;
    // Update is called once per frame
    public void spawnDialog()
    {
        Vector2 startPosition = plane.transform.position;
        for(int i = 0; i < dialogs.Count; i++)
        {
            GameObject go = dialogs[i];
            go.transform.position = startPosition + new Vector2((i * 100), 0);
            Instantiate(go);
        }
    }
}
