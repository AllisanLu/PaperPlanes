using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawStraightLine : MonoBehaviour
{
    public GameObject WindPrefab;
    public GameObject currentLine;
    public LineRenderer line;
    public BoxCollider boxCollider;
    // Start is called before the first frame update
    public Vector3 startMousePos;
    public Vector3 mousePos;

    public Vector3 endMousePos;
    public List<Vector2> positions;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetMouseButtonDown(0))
        {
            if(line == null)
                CreateLine();
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(0,mousePos);
            startMousePos = mousePos;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(line)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                line.SetPosition(1,mousePos);
                endMousePos = mousePos;
                addColliderToLine();
                line = null;
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if(line)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                line.SetPosition(1,mousePos);
            }
        }

    }

    void CreateLine() 
    {
        currentLine = Instantiate(WindPrefab, new Vector3(0,0,0), Quaternion.identity);
        line = currentLine.GetComponent<LineRenderer>();
        line.useWorldSpace = true;    

    }

    void addColliderToLine()
    {
        BoxCollider col = new GameObject("WindCollider").AddComponent<BoxCollider> ();
        col.transform.SetParent(currentLine.GetComponent<LineRenderer>().transform);
        float lineLength = Vector3.Distance (startMousePos, mousePos); // length of line
        col.size = new Vector2(lineLength, 0.25f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (startMousePos + mousePos)/2;
        currentLine.transform.position = midPoint;
        col.transform.position = midPoint; // setting position of collider object
        // Following lines calculate the angle between startPos and endPos
        float angle = (Mathf.Abs (startMousePos.y - mousePos.y) / Mathf.Abs (startMousePos.x - mousePos.x));
        if((startMousePos.y<mousePos.y && startMousePos.x>mousePos.x) || (mousePos.y<startMousePos.y && mousePos.x>startMousePos.x))
        {
            angle*=-1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan (angle);
        print(angle);
        col.transform.Rotate (0, 0, angle);
    }
}
