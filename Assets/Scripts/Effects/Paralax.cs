using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float startX;
    private float imageWidth;
    private float parallaxFactor; // maps Z values so that Z=5 -> p=1, Z=0 -> p=0

    private Camera cam;
    private SpriteRenderer spriteRenderer;
    private GameObject leftChild;
    private GameObject rightChild;

    public float pixelsPerUnit = 16;


    // Start is called before the first frame update
    void Start()
    {
        if (pixelsPerUnit == 0) {
            Debug.LogError("Pixels per Unit not set in paralax!");
        }

        startX = transform.position.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        imageWidth = spriteRenderer.bounds.size.x;
        parallaxFactor = (transform.position.z / 5);

        cam = Camera.main;

        // create left + right for looping
        leftChild = new GameObject("Left " + name);
        SpriteRenderer lsr = leftChild.AddComponent<SpriteRenderer>();
        lsr.sprite = spriteRenderer.sprite;
        leftChild.transform.position = transform.position - new Vector3(imageWidth, 0);
        leftChild.transform.parent = transform;

        rightChild = new GameObject("Right " + name);
        SpriteRenderer rsr = rightChild.AddComponent<SpriteRenderer>();
        rsr.sprite = spriteRenderer.sprite;
        rightChild.transform.position = transform.position + new Vector3(imageWidth, 0);
        rightChild.transform.parent = transform;
    }

    void FixedUpdate() // Update() makes the paralax really jittery for some reason
    {
        float temp     = cam.transform.position.x * (1 - parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;
 
        Vector3 newPosition = new Vector3(startX + distance, transform.position.y, transform.position.z);
 
        transform.position = newPosition;
        // transform.position = PixelPerfectClamp(newPosition, pixelsPerUnit);
 
        if (temp > startX + (imageWidth / 2))      startX += imageWidth;
        else if (temp < startX - (imageWidth / 2)) startX -= imageWidth;
    }

    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit) // from https://pavcreations.com/parallax-scrolling-in-pixel-perfect-2d-unity-games/
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(locationVector.x * pixelsPerUnit), Mathf.CeilToInt(locationVector.y * pixelsPerUnit), Mathf.CeilToInt(locationVector.z * pixelsPerUnit));
        return vectorInPixels / pixelsPerUnit;
    }
}
