using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProjectileController : EnemyController
{
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Moves cannon to the left at a constant rate in a straight line
    public override Vector2 GetMove() {
        return (Vector2) transform.position + (new Vector2(-1, 0) * speed);
    }
}
