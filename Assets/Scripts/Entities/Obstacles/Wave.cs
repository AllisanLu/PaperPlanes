using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField]
    float xDisplacement = -8f;
    [SerializeField]
    float bobSpeed;
    [SerializeField]
    float bobHeight;
    Vector3 pos;
    public static FMOD.Studio.EventInstance WaveSFX;
    private bool wave = false;

    void Start()
    {
        WaveSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Wave");

        pos = transform.position;
    }

    void Update()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            float newY = Mathf.Sin(Time.time * bobSpeed) * bobHeight + pos.y;
            transform.position = new Vector2(xDisplacement * Time.deltaTime + transform.position.x, newY);
        }  

    }

    void FixedUpdate() 
    {
        if (GetComponent<Renderer>().isVisible && !wave)
        {
            wave = true;
            WaveSFX.start();
        }  
    }
}
