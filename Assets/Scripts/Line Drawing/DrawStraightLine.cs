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
    public PauseMenu pause;

    private float strength = .5f;
    private float baseValue = 2f;
    void Start()
    {
        //baseValue = 5;
        if (strength == 0) {
            strength = 0.5f;
        }
        //strength = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isPaused())
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
                startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            }
            if (Input.GetMouseButton(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                windLength = Vector3.Distance(new Vector3(startMousePos.x, startMousePos.y, 0f), new Vector3(mousePos.x, mousePos.y, 0f));
                if (windLength * ResourceBar.instance.getWindScale() <= ResourceBar.instance.getCurrentResources()) 
                {
                    line.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
                    line.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
                } 
                else
                {
                   float ratio = ResourceBar.instance.getCurrentResources() / (ResourceBar.instance.getWindScale() * windLength);
                   Vector3 vector = Vector3.Scale(new Vector3(mousePos.x, mousePos.y, 0f) - new Vector3(startMousePos.x, startMousePos.y, 0f), new Vector3(ratio, ratio, 0));
                   Vector3 final = new Vector3(startMousePos.x, startMousePos.y, 0f) + vector;
                   line.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
                   line.SetPosition(1, final);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                line.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                windLength = Vector3.Distance(new Vector3(startMousePos.x, startMousePos.y, 0f), new Vector3(mousePos.x, mousePos.y, 0f));
                if (windLength * ResourceBar.instance.getWindScale() <= ResourceBar.instance.getCurrentResources()) 
                {
                    line.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
                    line.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
                } 
                else
                {
                   float ratio = ResourceBar.instance.getCurrentResources() / (ResourceBar.instance.getWindScale() * windLength);
                   Vector3 vector = Vector3.Scale(new Vector3(mousePos.x, mousePos.y, 0f) - new Vector3(startMousePos.x, startMousePos.y, 0f), new Vector3(ratio, ratio, 0));
                   Vector3 final = new Vector3(startMousePos.x, startMousePos.y, 0f) + vector;
                   line.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
                   line.SetPosition(1, final);
                }

                windLength = Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
                //Debug.Log(windLength);

                ResourceBar.instance.windResourceUsage(windLength);

                addColliderToLine(windLength, line.GetPosition(0), line.GetPosition(1));
                // line.SetWidth(0.5f, 0.5f);

            }
        }

    }

    void CreateLine()
    {
        currentLine = Instantiate(WindPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        positions.Clear();
        line = currentLine.GetComponent<LineRenderer>();
        line.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));

        line.useWorldSpace = true;

    }

    void addColliderToLine(float lineLength, Vector3 start, Vector3 end)
    {
        GameObject wind = new GameObject("WindCollider");
        WindCurrent windcurrent = wind.AddComponent<WindCurrent>();

        windcurrent.force = baseValue + (end - start).magnitude * strength;

        BoxCollider2D col = wind.AddComponent<BoxCollider2D>();

        col.isTrigger = true;
        col.transform.SetParent(currentLine.GetComponent<LineRenderer>().transform);
        //float lineLength = Vector3.Distance(startMousePos, mousePos); // length of line
        col.size = new Vector2(lineLength, 2f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (start + end) / 2;
        currentLine.transform.position = midPoint;
        col.transform.position = midPoint; // setting position of collider object
        // Following lines calculate the angle between startPos and endPos
        float angle = (Mathf.Abs(start.y - end.y) / Mathf.Abs(start.x - end.x));
        if ((start.y < end.y && start.x > end.x) || (end.y < start.y && end.x > start.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        if(windLength > 0) {
            col.transform.Rotate(0, 0, angle);
        }
    }
}
