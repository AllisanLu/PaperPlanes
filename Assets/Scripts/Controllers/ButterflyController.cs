using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : EnemyController
{
    public float speed;
    Butterfly butterfly;
    // Start is called before the first frame update
    void Start()
    {
        butterfly = GetComponent<Butterfly>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override Vector2 GetMove()
    {
        float horizontal = -2f;
        float y = 0.12f * Mathf.Sin(Time.time * 4) + transform.position.y;
        return new Vector2(horizontal * Time.deltaTime + transform.position.x, y);
    }
}
