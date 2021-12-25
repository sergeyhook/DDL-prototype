using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radioactive : MonoBehaviour
{
    public float RadiationAmount;
    public float Radius;
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().UnderRadiation = true;
            int layerMask = 1 << 9;
            layerMask = ~layerMask;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, collider.gameObject.transform.position, out hit, Mathf.Infinity, layerMask))
            {
                collider.gameObject.GetComponent<PlayerController>().RadiationAmount +=RadiationAmount* (Radius-Vector3.Distance(hit.point,this.transform.position));
                Debug.Log(hit.distance);
            }
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().UnderRadiation = false;
            collider.gameObject.GetComponent<PlayerController>().RadiationAmount = 0;
        }

    }
}

