using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerController : EnemyController
{
    public float speed;
    Banner banner;

    void Start()
    {
        banner = GetComponent<Banner>();
    }
    
    void Update()
    {

    }

    public override Vector2 GetMove()
    {
        return (Vector2) transform.position + (new Vector2(-1, 0) * speed);
    }
}