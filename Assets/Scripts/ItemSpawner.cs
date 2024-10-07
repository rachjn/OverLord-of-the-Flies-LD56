using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
   [System.Serializable]
    public struct SpawnRule { public GameObject Prefab; public float Weight; } 
    public GameObject[] powerUpPrefabs; // Array to hold different power-up prefabs
    public SpawnRule[] eggSpawnRules;
    public float powerUpSpawnInterval = 5.0f; // Time between power-up spawns
    public float eggSpawnInterval = 2.0f;  // Time interval between egg spawns
    public float spawnRadius = 2.0f;       // Radius to check for existing eggs or power-ups
    public Collider2D[] walls;             // Array of wall colliders to ensure spawn within walls
    public LayerMask itemLayer;            // Layer where items are placed to check for overlap
    public LayerMask obstacleLayer;        // Layer where obstacles are placed to check for overlap

    public int initialEggCount = 3;        // Number of eggs to spawn initially
    public float initialDelay = 1.5f;      // Delay before any spawning starts

    private void Start()
    {
        // Start the coroutine to delay initial spawning
        StartCoroutine(DelayedStart());
    }

  private IEnumerator DelayedStart()
{
    Debug.Log("DelayedStart called"); // Add this line
    // Spawn the initial 3 eggs
    Debug.Log("Spawning initial eggs...");
    SpawnInitialEggs();

    Debug.Log("Waiting for the initial delay...");
    yield return new WaitForSeconds(initialDelay);

    Debug.Log("Initial delay over, starting spawning...");
    StartCoroutine(SpawnEggs());
    StartCoroutine(SpawnPowerUps());
}
private void SpawnInitialEggs()
{
    int eggsSpawned = 0;
    int maxAttempts = 100; // Optional: Max attempts to prevent infinite loops

    while (eggsSpawned < initialEggCount && maxAttempts > 0)
    {
        Vector2 spawnPosition = GetRandomSpawnPositionWithinWalls();

        // Visual marker for the spawn attempt (optional, for debugging)
        Debug.DrawRay(spawnPosition, Vector2.up * 0.2f, Color.red, 5f);

        // Check for overlap with other items or obstacles
        bool positionValid = !Physics2D.OverlapCircle(spawnPosition, spawnRadius, itemLayer) &&
                             !Physics2D.OverlapCircle(spawnPosition, spawnRadius, obstacleLayer);

        if (positionValid)
        {
            Debug.Log("Spawning initial egg at position: " + spawnPosition);
            makeRandomEgg(spawnPosition);
            eggsSpawned++;
        }
        else
        {
            Debug.Log("Overlap detected (item or obstacle), retrying...");
        }

        maxAttempts--;
    }

    Debug.Log("Total Eggs Spawned: " + eggsSpawned);
}

    private void makeRandomEgg(Vector2 spawnPosition)
    {
        var thing = Random.value;
        GameObject picked = null;
        foreach (var rule in eggSpawnRules)
        {
            var prefab = rule.Prefab;
            var weight = rule.Weight;
            if (thing <= weight)
            {
                picked = prefab;
                break;
            }
            thing -= weight;
        }
        if (picked != null)
        {
            var egg = Instantiate(picked, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator SpawnEggs()
    {
        while (true)
        {
            yield return new WaitForSeconds(eggSpawnInterval); // Wait for the next egg spawn interval
            Vector2 spawnPosition = GetRandomSpawnPositionWithinWalls();

            // Check if there's already an item (egg/powerup) or an obstacle at the spawn position using OverlapCircle
            if (!Physics2D.OverlapCircle(spawnPosition, spawnRadius, itemLayer) &&
                !Physics2D.OverlapCircle(spawnPosition, spawnRadius, obstacleLayer)) // Added obstacle check
            {
                Debug.Log("Spawning egg at position: " + spawnPosition);
                makeRandomEgg(spawnPosition);
            }
            else
            {
                Debug.Log("Overlap detected (item or obstacle), skipping egg spawn.");
            }
        }
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnInterval); // Wait for the next power-up spawn interval
            
            Vector2 spawnPosition = GetRandomSpawnPositionWithinWalls();

            // Check if there's already an item (egg/powerup) or an obstacle at the spawn position using OverlapCircle
            if (!Physics2D.OverlapCircle(spawnPosition, spawnRadius, itemLayer) &&
                !Physics2D.OverlapCircle(spawnPosition, spawnRadius, obstacleLayer)) // Added obstacle check
            {
                GameObject powerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
                Debug.Log("Spawning power-up at position: " + spawnPosition);
                Instantiate(powerUp, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.Log("Overlap detected (item or obstacle), skipping power-up spawn.");
            }
        }
    }

    Vector2 GetRandomSpawnPositionWithinWalls()
    {
        // Define boundaries within the walls
        // float xMin = walls[0].bounds.min.x - 6f;
        // float xMax = walls[1].bounds.max.x + 6f;
        // float yMin = walls[2].bounds.min.y - 7.2f;
        // float yMax = walls[3].bounds.max.y + 7.2f;
        float xMin = walls[0].bounds.min.x+1;
        float xMax = walls[1].bounds.max.x-1;
        float yMin = walls[2].bounds.min.y+1;
        float yMax = walls[3].bounds.max.y-1;

        // Debug.Log("WALL POSITIONS: " + xMin + xMax + yMin + yMax);


        // Generate random X and Y coordinates within the walls
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);

        return new Vector2(x, y);
    }
}
