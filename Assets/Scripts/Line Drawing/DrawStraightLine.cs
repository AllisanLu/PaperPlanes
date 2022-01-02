using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawStraightLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    // Start is called before the first frame update
    public Vector2 startMousePos;
    public Vector2 mousePos;
    public List<Vector2> positions;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            CreateLine();
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if(Input.GetMouseButton(0)) 
        {
            
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            lineRenderer.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
            
            edgeCollider.points = positions.ToArray();

        }
    }

    void CreateLine() 
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        positions.Clear();
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

    }
}
