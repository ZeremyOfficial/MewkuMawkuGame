using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int attackDamage = 10;
    public GameObject deathEffectPrefab;
    public float deathEffectDuration = 2.0f;

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
        // Spawn the death effect prefab at the enemy's position
        if (deathEffectPrefab)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

            // Destroy the death effect after a set duration
            Destroy(deathEffect, deathEffectDuration);
        }

        if (ScoreScript.instance != null)
        {
            ScoreScript.instance.AddPoints(10);
        }
        else
        {
            Debug.LogError("ScoreScript instance not found.");
        }

        Destroy(gameObject);
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