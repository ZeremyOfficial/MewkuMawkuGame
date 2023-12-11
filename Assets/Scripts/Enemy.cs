using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int attackDamage = 10;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        if (ScoreScript.instance != null)
        {
            ScoreScript.instance.AddPoints(10); // Use the singleton instance
        }
        else
        {
            Debug.LogError("ScoreScript instance not found.");
        }

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
