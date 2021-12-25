using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Transform Target;
    private Rigidbody rb;
    public float turnSpeed = 1f;
    public float rocketSpeed = 1f;
    private Transform rocketLocalTrans;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        rocketLocalTrans = GetComponent<Transform>();

    }
    void Update()
    {

        Vector3 dir = Target.position - transform.position;
        float distanceThisFrame = rocketSpeed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(Target);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
