using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int attackDamage = 10;

    // Reference to the ScoreScript
    private ScoreScript scoreScript;

    void Start()
    {
        currentHealth = maxHealth;

        // Find the ScoreScript in the scene
        scoreScript = GameObject.Find("ScoreManager").GetComponent<ScoreScript>();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Optional: Add any reaction to taking damage here (e.g., play a hit animation)

        if (currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        // Optional: Add death animation or effects here

        // Award points to the player when the enemy is defeated
        scoreScript.AddPoints(10); // Change the points awarded as needed

        Destroy(gameObject); // Destroy the enemy object
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.IsInvincible)
            {
                playerHealth.PlayerTakeDamage(attackDamage);
            }
        }
    }
}