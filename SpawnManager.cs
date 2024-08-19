using System;
using System.Numerics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] spawnPoints;

    public int maxEnemies = 5;
    private int currentEnemies = 0;

    public float spawnDelay = 2.0f;
    private float currentSpawnDelay = 0.0f;

    void Update()
    {
        // Check if we need to spawn a new enemy
        if (currentEnemies < maxEnemies && currentSpawnDelay <= 0.0f)
        {
            // Choose a random spawn point
            int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            UnityEngine.Vector3 spawnPosition = spawnPoints[spawnIndex].transform.position;

            // Spawn a new enemy at the chosen spawn point
            Instantiate(enemyPrefab, spawnPosition, UnityEngine.Quaternion.identity);

            // Increase the current enemy count and reset the spawn delay
            currentEnemies++;
            currentSpawnDelay = spawnDelay;
        }

        // Decrease the spawn delay timer
        if (currentSpawnDelay > 0.0f)
        {
            currentSpawnDelay -= Time.deltaTime;
        }
    }

    // Call this function to decrease the current enemy count
    public void EnemyKilled()
    {
        currentEnemies--;
    }
}