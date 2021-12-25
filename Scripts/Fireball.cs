using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    float Damage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        Damage = player.GetComponent<PlayerController>().FireballDamage;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * 10f);
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.transform.GetComponent<EnemyScript>().HP -= Damage;
                Destroy(this.gameObject);
            }
        }
    }
}
