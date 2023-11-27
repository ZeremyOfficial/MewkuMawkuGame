using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10; // Damage the fireball deals to enemies
    public float maxRange = 10f; // Maximum range the fireball can travel

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position; // Record the starting position
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Check if the fireball has exceeded its maximum range
        if (Vector2.Distance(startPosition, transform.position) > maxRange)
        {
            Destroy(gameObject); // Destroy the fireball
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy the fireball after it hits an enemy
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject); // Destroy the fireball if it hits an obstacle
        }
        // Additional collision handling as necessary
    }
}