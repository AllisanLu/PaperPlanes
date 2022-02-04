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
    public float windLength;
    public ResourceBar resourceBar;

    public float strength;
    void Start()
    {
        strength = 0.2f;
    }

    // Update is called once per frame
     void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            CreateLine();
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        if(Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            line.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
        }
        if(Input.GetMouseButtonUp(0)) 
        {
            
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            line.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            line.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));

            windLength = Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
            Debug.Log(windLength);
            
            ResourceBar.instance.windResourceUsage(windLength);

            addColliderToLine();

        }
        }   


    }

    void CreateLine() 
    {
        currentLine = Instantiate(WindPrefab, new Vector3(0,0,0), Quaternion.identity);
        positions.Clear();
        line = currentLine.GetComponent<LineRenderer>();
        line.useWorldSpace = true;    

    }

    void addColliderToLine()
    {
        GameObject wind = new GameObject("WindCollider");
        WindCurrent windcurrent = wind.AddComponent<WindCurrent>();

        windcurrent.force = (mousePos - startMousePos).magnitude * strength;

        BoxCollider2D col = wind.AddComponent<BoxCollider2D> ();
        col.isTrigger = true;
        col.transform.SetParent(currentLine.GetComponent<LineRenderer>().transform);
        float lineLength = Vector3.Distance (startMousePos, mousePos); // length of line
        col.size = new Vector2(lineLength, 0.5f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
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
        //print(angle);
        col.transform.Rotate (0, 0, angle);

    }
}
