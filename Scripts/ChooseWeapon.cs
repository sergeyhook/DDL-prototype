using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChooseWeapon : MonoBehaviour
{
    public GameObject spawner;
    public HealthBar EnemyBar;
    public int seletedWeapon;
    public float ShootTime;
    public float BPM;
    public float CostToShoot;
    void Update()
    {
        ShootTime = transform.GetChild(seletedWeapon).GetComponent<LongRange>().ShotTime;
        BPM = transform.GetChild(seletedWeapon).GetComponent<LongRange>().BPM;
        CostToShoot = transform.GetChild(seletedWeapon).GetComponent<LongRange>().CostToShoot;
    }
    public void SelectWeapon(int selectedWeapon) 
    {
        seletedWeapon = selectedWeapon;
        // убираем все и выбираем одно оружие метод публичный
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != 4)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(selectedWeapon).gameObject.SetActive(true);
    }
    public void Shoot()
    {
        RaycastHit Hit;
        int layerMask = (1 << 2);
        //Invert to ignore it
        layerMask = ~layerMask;
        GameObject muzzleflash = Instantiate(transform.GetChild(seletedWeapon).GetComponent<LongRange>().muzzle, transform.GetChild(seletedWeapon).GetComponent<LongRange>().spawnpoint.position, transform.GetChild(seletedWeapon).GetComponent<LongRange>().spawnpoint.rotation);
        if (Physics.Raycast(spawner.transform.position, spawner.transform.forward, out Hit, transform.GetChild(seletedWeapon).GetComponent<LongRange>().distance,layerMask))
        {
            Instantiate(transform.GetChild(seletedWeapon).GetComponent<LongRange>().impact, Hit.point, Quaternion.LookRotation(Hit.normal));

            if (Hit.transform.tag == "Enemy")
            {
                EnemyBar.SetMaxHealth(Hit.transform.GetComponent<EnemyScript>().HP);
                Hit.transform.GetComponent<EnemyScript>().HP -= transform.GetChild(seletedWeapon).GetComponent<LongRange>().Damage;
                EnemyBar.SetHealth(Hit.transform.GetComponent<EnemyScript>().HP);
            }

        }
    }
}
