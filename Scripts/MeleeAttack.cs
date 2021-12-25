using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float HP;
    Animator anim;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("attacking", true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("attacking", false);
        }
    }
    
}
