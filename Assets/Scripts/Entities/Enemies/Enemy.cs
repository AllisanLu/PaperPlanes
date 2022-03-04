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
        Physics2D.IgnoreLayerCollision(7, 7, true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GetComponent<Renderer>().isVisible)
        {
            Move();
        }
    }

    public virtual void Move()
    {

    }
}
