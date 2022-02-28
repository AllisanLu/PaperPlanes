using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public int damage;
    public EnemyController behaviorController = null;
    public Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        print("runs this too");
        Physics2D.IgnoreLayerCollision(7, 7, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
