using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonball;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator fireProjectile() {
        anim.Play("cannon-fire");
        yield return new WaitForSeconds(.2f);
        Instantiate(cannonball, this.transform.position, Quaternion.identity, transform);
    }
}
