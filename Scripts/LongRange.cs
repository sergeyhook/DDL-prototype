using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRange : MonoBehaviour
{
    public float ReloadTime;
    public float Damage;
    public Transform spawnpoint;
    public float distance = 15f;
    public GameObject muzzle;
    public GameObject impact;
    public float BPM;
    public float ShotTime;
    public float CostToShoot;
    void Start()
    {
        ShotTime = 60 / BPM;
    }
}
