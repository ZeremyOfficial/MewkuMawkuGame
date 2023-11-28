using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to your enemy prefab
    public Transform player; // Reference to the player character's Transform
    public Vector2 minPosition;
    public Vector2 maxPosition;
    public float spawnInterval = 2.0f; // Time between enemy spawns
    public int maxEnemies = 10; // Maximum number of enemies to spawn

    private int enemiesSpawned = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < maxEnemies)
        {
            if (player != null)
            {
                float randomX = Random.Range(minPosition.x, maxPosition.x);
                float randomY = Random.Range(minPosition.y, maxPosition.y);

                Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                newEnemy.GetComponent<EnemyMovement>().target = player;

                enemiesSpawned++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}