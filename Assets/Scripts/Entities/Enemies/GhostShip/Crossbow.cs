using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public GameObject crossbow;
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
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(.6f);
        Instantiate(crossbow, this.transform.position, Quaternion.identity, transform);
        crossbow.layer = LayerMask.NameToLayer("Enemy");
    }
}
