using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globals : MonoBehaviour
{
    public bool PlayerAlive = true;
    public GameObject playe;
    public int Enemies;
    public float PlayerMoney;
    void Start()
    {
        if(PlayerAlive == false)
        {
            GameObject player = Instantiate(playe);
            player.transform.position = new Vector3(0, 2f, 0);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
