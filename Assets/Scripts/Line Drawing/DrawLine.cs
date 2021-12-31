using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> cursorPositions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            CreateLine();
        }
        if(Input.GetMouseButton(0))
        {
            Vector2 tempCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempCursorPos, cursorPositions[cursorPositions.Count - 1]) > .1f) 
            {
                UpdateLine(tempCursorPos);
            }
        }
    }

    void CreateLine() {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        cursorPositions.Clear();
        cursorPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        cursorPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0,cursorPositions[0]);
        lineRenderer.SetPosition(1,cursorPositions[1]);
        edgeCollider.points = cursorPositions.ToArray();
    }

    void UpdateLine(Vector2 newCursorPos) 
    {
        cursorPositions.Add(newCursorPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newCursorPos);
        edgeCollider.points = cursorPositions.ToArray();
    }
}
