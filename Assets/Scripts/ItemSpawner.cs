using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public GameObject eggPrefab;    // The egg prefab
    public GameObject[] powerUpPrefabs; // Array to hold different power-up prefabs
    public float powerUpSpawnInterval = 5.0f; // Time between power-up spawns

    public float spawnRate = 2.0f;  // Time interval between spawns
    public float spawnRadius = 1.0f; // Radius to check for existing eggs
    public Collider2D[] walls;      // Array of wall colliders to ensure spawn within walls
    public LayerMask itemLayer;      // Layer where items are placed to check for overlap

    private void Start()
    {
        // Start spawning eggs at intervals
        InvokeRepeating("SpawnEgg", 0f, spawnRate);
        StartCoroutine(SpawnPowerUps());
    }

    void SpawnEgg()
    {
        Vector2 spawnPosition;

        // Try finding a valid spawn position that isn't overlapping
        int maxAttempts = 10; // Limit the number of attempts to prevent infinite loops
        for (int i = 0; i < maxAttempts; i++)
        {
            spawnPosition = GetRandomSpawnPositionWithinWalls();

            // Check if there's already an egg at the spawn position using OverlapCircle
            if (!Physics2D.OverlapCircle(spawnPosition, spawnRadius, itemLayer))
            {
                // No overlapping object, so spawn the egg
                Instantiate(eggPrefab, spawnPosition, Quaternion.identity);
                break;
            }
        }
    }

     private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            Vector3 spawnPosition = GetRandomSpawnPositionWithinWalls();
            GameObject powerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
            Instantiate(powerUp, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(powerUpSpawnInterval); // Wait before spawning the next power-up
        }
    }

    Vector2 GetRandomSpawnPositionWithinWalls()
    {
        // Define boundaries within the walls
        float xMin = walls[0].bounds.min.x;
        float xMax = walls[1].bounds.max.x;
        float yMin = walls[2].bounds.min.y;
        float yMax = walls[3].bounds.max.y;

        // Generate random X and Y coordinates within the walls
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);

        return new Vector2(x, y);
    }
}
