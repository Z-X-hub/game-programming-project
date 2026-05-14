using System.Collections.Generic;
using UnityEngine;

public class ArcadeEnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    public GameObject hazardPrefab;
    public Transform enemyHolder;
    public GameObject powerUpPrefab;
    public float spawnY = 5.7f;
    public float spawnXRange = 8.2f;
    public float baseSpawnDelay = 1.8f;
    public float minimumSpawnDelay = 0.55f;
    public int baseMaxAlive = 6;
    public float firstSpawnDelay = 1.5f;
    public bool allowBoss = true;
    public int bossAfterDefeats = 9;
    public bool spawnHazards = true;
    public float hazardSpawnDelay = 4.5f;
    public float firstHazardDelay = 7f;

    private float nextSpawnTime;
    private float nextHazardTime;
    private bool bossSpawned;
    private readonly List<GameObject> aliveEnemies = new List<GameObject>();

    private void Start()
    {
        nextSpawnTime = Time.time + firstSpawnDelay;
        nextHazardTime = Time.time + firstHazardDelay;
    }

    private void Update()
    {
        if (Health.instance != null && !Health.instance.CanPlay)
        {
            return;
        }

        CleanEnemyList();

        float difficulty = Health.instance != null ? Health.instance.DifficultyMultiplier : 1f;
        int maxAlive = baseMaxAlive + Mathf.FloorToInt(Mathf.Max(0f, difficulty - 1f));
        float spawnDelay = Mathf.Max(minimumSpawnDelay, baseSpawnDelay / difficulty);

        if (allowBoss && !bossSpawned && bossPrefab != null && Health.instance != null && Health.instance.EnemiesDefeated >= bossAfterDefeats)
        {
            SpawnSpecificEnemy(bossPrefab, new Vector3(0f, spawnY, 0f));
            bossSpawned = true;
            Health.instance.Announce("Boss ship entering the sector", 2.5f);
            CameraShake.Shake(0.2f, 0.18f);
            nextSpawnTime = Time.time + spawnDelay;
        }

        if (Time.time >= nextSpawnTime && aliveEnemies.Count < maxAlive)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnDelay;
        }

        if (spawnHazards && hazardPrefab != null && Time.time >= nextHazardTime)
        {
            SpawnHazard();
            nextHazardTime = Time.time + Mathf.Max(1.5f, hazardSpawnDelay / difficulty);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            return;
        }

        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnXRange, spawnXRange), spawnY, 0f);
        SpawnSpecificEnemy(prefab, spawnPosition);
    }

    private void SpawnSpecificEnemy(GameObject prefab, Vector3 spawnPosition)
    {
        GameObject enemy = Instantiate(prefab, spawnPosition, prefab.transform.rotation);
        if (enemyHolder != null)
        {
            enemy.transform.SetParent(enemyHolder);
        }

        ArcadeEnemy arcadeEnemy = enemy.GetComponent<ArcadeEnemy>();
        if (arcadeEnemy != null && arcadeEnemy.powerUpPrefab == null)
        {
            arcadeEnemy.powerUpPrefab = powerUpPrefab;
        }

        aliveEnemies.Add(enemy);
    }

    private void SpawnHazard()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnXRange, spawnXRange), spawnY + 0.4f, 0f);
        GameObject hazard = Instantiate(hazardPrefab, spawnPosition, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        if (enemyHolder != null)
        {
            hazard.transform.SetParent(enemyHolder);
        }
    }

    private void CleanEnemyList()
    {
        for (int i = aliveEnemies.Count - 1; i >= 0; i--)
        {
            if (aliveEnemies[i] == null)
            {
                aliveEnemies.RemoveAt(i);
            }
        }
    }
}
