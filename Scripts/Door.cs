using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Door : MonoBehaviour
{
   // public TMP_Text text;
    public float CostToOpen;
    //public GameObject Dor;
    RaycastHit hit;
    Collider m_Collider;
    public Renderer rend;
    public bool DoorToOpen;
    public bool Render;
    public bool Collider;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        m_Collider = GetComponent<Collider>();
        Physics.Raycast(transform.position, transform.forward, out hit, 1f);
        if (hit.collider != null && hit.collider.CompareTag("Door"))
        {
            DoorToOpen = true;
            Destroy(hit.collider.gameObject);
            //Instantiate(Dor, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
   /* private void Update()
    {
        Render = rend.enabled;
        Collider = m_Collider.enabled;
        if (DoorToOpen == true)
        {
            rend.enabled = false;
            m_Collider.enabled = false;
        }
        else
        {
            rend.enabled = true;
            m_Collider.enabled = true;
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            rend.enabled = true;
            m_Collider.enabled = true;
        }
    }*/
}
