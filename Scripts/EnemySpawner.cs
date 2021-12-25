using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public int minEnemies;
    public int maxEnemies;
    public float MaxSizeOfRoomX;
    public float MaxSizeOfRoomY;
    public int SpawnedEnemies;
    public int ChanceToSpawn;
    void Start()
    {
        int AmountOfEnemies = Random.Range(minEnemies, maxEnemies);
        for (int i = 0; i < MaxSizeOfRoomX; i++)
        {
            for (int j = 0; j < MaxSizeOfRoomY; j++)
            {
                int l = Random.Range(0, 10);
                int R = Random.Range(0, Enemies.GetLength(0));
                if (SpawnedEnemies < AmountOfEnemies && l>ChanceToSpawn)
                {
                    Instantiate(Enemies[R], transform.position + new Vector3(i,0.5f,j),transform.rotation);
                    SpawnedEnemies += 1;
                }
            }
        }
    }
}
