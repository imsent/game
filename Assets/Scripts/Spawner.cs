using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnEnemy;

    [SerializeField] private Transform[] SpawnPoint;
    
    public int bat1;

    public int bat2;

    public int bat3;
    
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (bat1 < 1)
        {
            Instantiate(spawnEnemy[0], SpawnPoint[Random.Range(0, SpawnPoint.Length)].position, Quaternion.identity);
            bat1++;
            return;
        }
        if (bat2 < 2)
        {
            Instantiate(spawnEnemy[1], SpawnPoint[Random.Range(0, SpawnPoint.Length)].position, Quaternion.identity);
            bat2++;
            return;
        }
        if (bat3 < 3)
        {
            Instantiate(spawnEnemy[2], SpawnPoint[Random.Range(0, SpawnPoint.Length)].position, Quaternion.identity);
            bat3++;
            return;
        }
    }
}
