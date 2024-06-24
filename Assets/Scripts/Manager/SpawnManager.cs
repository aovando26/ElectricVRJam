using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;

    private int intervalOfX = 10;
    private int spawnPosZ = -20;
    private float startDelay = 2.0f;
    private float spawnInterval = 5.0f;

    void Start()
    {
        InvokeRepeating("SpawnRandomZombie", startDelay, spawnInterval);
    }

    void Update()
    { 
    
    }

    void SpawnRandomZombie()
    {
        int indexCount = Random.Range(0, zombiePrefabs.Length);

        int randomX = Random.Range(-intervalOfX, intervalOfX);

        Vector3 spawnPos = new Vector3(randomX, 1.5f, spawnPosZ);

        Instantiate(zombiePrefabs[indexCount], spawnPos, zombiePrefabs[indexCount].transform.rotation);
    }
}
