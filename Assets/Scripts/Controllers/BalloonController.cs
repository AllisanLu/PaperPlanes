using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if you need to do any math for moving per frame do it here
    }

    public override Vector2 GetMove() {
        return (Vector2) transform.position + new Vector2(0, 5);
    }
}
