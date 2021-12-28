using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globals : MonoBehaviour
{
    public bool PlayerAlive = true;
    public GameObject playe;
    public int Enemies;
    public float PlayerMoney;
    public GameObject canvas;
    void Start()
    {
        
        DontDestroyOnLoad(this.gameObject);
    }
}
