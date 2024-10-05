using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public GameObject eggPrefab;       // The egg prefab
    public GameObject[] powerUpPrefabs; // Array to hold different power-up prefabs
    public float powerUpSpawnInterval = 5.0f; // Time between power-up spawns

    public float eggSpawnInterval = 2.0f;  // Time interval between egg spawns
    public float spawnRadius = 2.0f;       // Radius to check for existing eggs or power-ups
    public Collider2D[] walls;             // Array of wall colliders to ensure spawn within walls
    public LayerMask itemLayer;            // Layer where items are placed to check for overlap

    public int initialEggCount = 3;        // Number of eggs to spawn initially
    public float initialDelay = 1.5f;      // Delay before any spawning starts

    private void Start()
    {
        // Start the coroutine to delay initial spawning
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {

        // Spawn the initial 3 eggs
        SpawnInitialEggs();

        Debug.Log("Waiting for the initial delay...");
        // Wait for the initial delay (3 seconds by default)
        yield return new WaitForSeconds(initialDelay);

        Debug.Log("Initial delay over, starting spawning...");

        // After spawning the initial eggs, start spawning eggs and power-ups at intervals
        StartCoroutine(SpawnEggs());
        StartCoroutine(SpawnPowerUps());
    }

    private void SpawnInitialEggs()
    {
        int eggsSpawned = 0;
        while (eggsSpawned < initialEggCount)
        {
            Vector2 spawnPosition = GetRandomSpawnPositionWithinWalls();

            // Check if there's already an item (egg/powerup) at the spawn position using OverlapCircle
            if (!Physics2D.OverlapCircle(spawnPosition, spawnRadius, itemLayer))
            {
                Debug.Log("Spawning initial egg at position: " + spawnPosition);
                Instantiate(eggPrefab, spawnPosition, Quaternion.identity);
                eggsSpawned++;
            }
            else
            {
                Debug.Log("Overlap detected, trying to spawn egg again...");
            }
        }
    }

    private IEnumerator SpawnEggs()
    {
        while (true)
        {
            yield return new WaitForSeconds(eggSpawnInterval); // Wait for the next egg spawn interval
            Vector2 spawnPosition = GetRandomSpawnPositionWithinWalls();

            // Check if there's already an item (egg/powerup) at the spawn position using OverlapCircle
            if (!Physics2D.OverlapCircle(spawnPosition, spawnRadius, itemLayer))
            {
                Debug.Log("Spawning egg at position: " + spawnPosition);
                Instantiate(eggPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.Log("Overlap detected, skipping egg spawn.");
            }
        }
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnInterval); // Wait for the next power-up spawn interval
            
            Vector2 spawnPosition = GetRandomSpawnPositionWithinWalls();

            // Check if there's already an item (egg/powerup) at the spawn position using OverlapCircle
            if (!Physics2D.OverlapCircle(spawnPosition, spawnRadius, itemLayer))
            {
                GameObject powerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
                Debug.Log("Spawning power-up at position: " + spawnPosition);
                Instantiate(powerUp, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.Log("Overlap detected, skipping power-up spawn.");
            }
        }
    }

    Vector2 GetRandomSpawnPositionWithinWalls()
    {
        // Define boundaries within the walls
        float xMin = walls[0].bounds.min.x - 1.0f;
        float xMax = walls[1].bounds.max.x + 1.0f;
        float yMin = walls[2].bounds.min.y  - 1.0f;
        float yMax = walls[3].bounds.max.y + 1.0f;

        // Generate random X and Y coordinates within the walls
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);

        return new Vector2(x, y);
    }
}
