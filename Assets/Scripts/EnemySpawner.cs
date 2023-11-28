using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Skelly prefab
    public float spawnInterval = 5f; // Time interval between spawns
    private float timer = 0f;
    private int enemyCount = 1; // Initial number of enemies to spawn

    // Camera boundary limits for 2D
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Additional variables for validity check
    public float spawnRadius = 1.0f; // Radius to check for spawn validity

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemies(enemyCount);
            timer = 0f;
            enemyCount++; // Increase the number of enemies over time
        }
    }

    void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = CalculateRandomPositionWithinBounds();
            if (IsValidSpawnPosition(spawnPosition))
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 CalculateRandomPositionWithinBounds()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector3(x, y, 0); // Assuming Z is 0 for a 2D game
    }

    bool IsValidSpawnPosition(Vector3 position)
    {
        // Check if there are any colliders within the spawn radius
        Collider[] hitColliders = Physics.OverlapSphere(position, spawnRadius);
        if (hitColliders.Length > 0)
        {
            // If there are colliders, the position is not valid for spawning
            return false;
        }
        return true;
    }
}
