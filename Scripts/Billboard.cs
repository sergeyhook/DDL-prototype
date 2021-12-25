using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    void LateUpdate()
    {
        GameObject Player = GameObject.Find("Player");
        cam = Player.transform.GetChild(1);
        transform.LookAt(transform.position+cam.forward);
    }
}
