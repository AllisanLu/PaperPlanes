using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloudController : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Vector2 GetMove() {
        return (Vector2) transform.position;
    }
}
