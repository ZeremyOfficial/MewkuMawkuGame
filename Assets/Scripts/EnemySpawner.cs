using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Skelly prefab
    public float spawnInterval = 5f; // Time interval between spawns
    private float timer = 0f;

    public int initialEnemyCount = 1; // Initial number of enemies to spawn
    public int currentEnemyCount; // The current number of enemies to spawn
    public int maxEnemyCount = 10; // Maximum number of enemies to spawn
    public float enemyCountIncreaseRate = 1.1f; // Rate of increase in enemy count per spawn

    public float initialMoveSpeed = 1.0f; // Initial movement speed of enemies
    public float moveSpeedIncreaseRate = 1.05f; // Rate of increase in movement speed per spawn
    public float maxMoveSpeed = 5.0f; // Maximum movement speed for enemies
    private float currentMoveSpeed; // Current movement speed for spawned enemies

    // Camera boundary limits for 2D
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Additional variables for validity check
    public float spawnRadius = 1.0f; // Radius to check for spawn validity

    private bool canSpawn = true; // Control flag for spawning new enemies

    void Start()
    {
        currentEnemyCount = initialEnemyCount;
        currentMoveSpeed = initialMoveSpeed;
    }

    void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnEnemies(currentEnemyCount);
                timer = 0f;
                IncreaseEnemyCount();
                IncreaseMoveSpeed();
            }
        }
    }

    void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = CalculateRandomPositionWithinBounds();
            if (IsValidSpawnPosition(spawnPosition))
            {
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                newEnemy.GetComponent<EnemyMovement>().SetMoveSpeed(currentMoveSpeed);
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
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, spawnRadius);
        return hitColliders.Length == 0;
    }

    void IncreaseEnemyCount()
    {
        currentEnemyCount = Mathf.Min(maxEnemyCount, Mathf.FloorToInt(currentEnemyCount * enemyCountIncreaseRate));
    }

    void IncreaseMoveSpeed()
    {
        currentMoveSpeed = Mathf.Min(maxMoveSpeed, currentMoveSpeed * moveSpeedIncreaseRate);
    }

    // Public method to control the spawning process
    public void ToggleSpawning(bool status)
    {
        canSpawn = status;
    }
}
