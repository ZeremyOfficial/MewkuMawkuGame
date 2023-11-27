using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Player's transform
    public float moveSpeed = 3.0f; // Speed at which the enemy moves

    private void Update()
    {
        // Move towards the player
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player and move towards them
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
