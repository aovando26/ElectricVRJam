using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public Transform xrOriginTransform;
    private int intervalOfX = 10;
    private int spawnPosZ = -20;

    private WaveManager waveManager;
    private Coroutine spawnCoroutine;

    public int enemyMaxHealth = 100;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        waveManager = WaveManager.Instance;
        if (waveManager == null)
        {
            Debug.LogError("WaveManager not found in the scene!");
            return;
        }

        waveManager.OnWaveStart += StartSpawning;
        waveManager.OnWaveEnd += StopSpawning;
    }

    void StartSpawning(int waveNumber)
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    void StopSpawning(int waveNumber)
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (waveManager.CanSpawnEnemy())
            {
                SpawnRandomZombie();
                yield return new WaitForSeconds(waveManager.GetCurrentSpawnInterval());
            }
            else
            {
                yield return null;
            }
        }
    }

    void SpawnRandomZombie()
    {
        int indexCount = Random.Range(0, zombiePrefabs.Length);
        int randomX = Random.Range(-intervalOfX, intervalOfX);
        Vector3 spawnPos = new Vector3(randomX, 1.5f, spawnPosZ);
        GameObject newZombie = Instantiate(zombiePrefabs[indexCount], spawnPos, zombiePrefabs[indexCount].transform.rotation);

        WanderingAI wanderingAI = newZombie.GetComponent<WanderingAI>();
        if (wanderingAI != null)
        {
            wanderingAI.target = xrOriginTransform;
        }

        EnemyWrapper enemyWrapper = newZombie.GetComponent<EnemyWrapper>();
        if (enemyWrapper == null)
        {
            enemyWrapper = newZombie.AddComponent<EnemyWrapper>();
        }
        enemyWrapper.Initialize(enemyMaxHealth, OnEnemyDeath);

        activeEnemies.Add(newZombie);
    }

    void OnEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        waveManager.EnemyKilled();
        Destroy(enemy);
    }

    public void DamageEnemy(EnemyWrapper enemyWrapper, int damage)
    {
        if (enemyWrapper != null)
        {
            enemyWrapper.TakeDamage(damage);
            Debug.Log($"Enemy took {damage} damage. Current health: {enemyWrapper.GetHealth()}");
        }
    }

    void OnDestroy()
    {
        if (waveManager != null)
        {
            waveManager.OnWaveStart -= StartSpawning;
            waveManager.OnWaveEnd -= StopSpawning;
        }
    }
}