using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float MaxSpeed;
    public GameObject Player;
    public float Distance;
    public bool IsFollowing;
    public NavMeshAgent agent;
    public int MaxDistance;
    public float MaxHP;
    public float HP;
    public float Damage;
    public float TimeToHit;
    public float Timer;
    public bool IsReady = true;
    public GameObject ItemDrop1;
    public GameObject ItemDrop2;
    public bool MeleeAttack;
    public GameObject Missle;
    public float ShootDistance;
    public bool IsReadyToShoot;
    public float XPDrop;
    public GameObject BackgroundVar;
    // Start is called before the first frame update
    void Start()
    {
        float speed = Random.Range(4, MaxSpeed);
        if (speed < 7)
        {
            HP = MaxHP+100;
        }
        else
        {
            HP = MaxHP;
        }
        agent.speed = speed;
        Player = GameObject.Find("Player");
        BackgroundVar = GameObject.Find("BackgroundScripts");
        BackgroundVar.GetComponent<globals>().Enemies+=1;
    }

    // Update is called once per frame
    void Update()
    {

        
        if (HP > 0)
        {
            if (MeleeAttack == false)
            {
                IsReadyToShoot = true;
            }
            
            if (Timer > TimeToHit & IsReady == false)
            {
                IsReady = true;
                Timer = 0f;
            }
            else
            {
                Timer += Time.deltaTime;
            }
            Distance = Vector3.Distance(Player.transform.position, this.transform.position);
            if(Distance<= ShootDistance && IsReadyToShoot&& IsReady)
            {
                Instantiate(Missle, this.transform.position+ new Vector3(0,0,1f), this.transform.rotation);
                Timer = 0f;
                IsReady = false;
            }
            if (Distance <= MaxDistance)
            {
                IsFollowing = true;
            }
            else
            {
                IsFollowing = false;
            }
            if (IsFollowing)
            {
                agent.isStopped = false;
                agent.SetDestination(Player.transform.position);
            }
            else
            {
                agent.isStopped = true;
            }
        }
        else
        {
            BackgroundVar.GetComponent<globals>().Enemies -= 1;
            int Chooser = Random.Range(0, 10); 
            if (Chooser>6)
            {
                Instantiate(ItemDrop1, this.transform.position, this.transform.rotation);
            }
            Chooser = Random.Range(0, 5);
            for (int i = 0; i < Chooser; i++)
            {
                Instantiate(ItemDrop2, this.transform.position, this.transform.rotation);
            }
            Player.GetComponent<PlayerController>().Xp += XPDrop;
            Destroy(this.gameObject);
        }
        
    }
   
    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player" & IsReady)
        {
            other.gameObject.GetComponent<PlayerController>().HP -= Damage;
            IsReady = false;
            Timer = 0f;
        }
    }
}
