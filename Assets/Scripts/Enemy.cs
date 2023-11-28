using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        
        // Optional: Add any reaction to taking damage here (e.g., play a hit animation)

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Optional: Add death animation or effects here

        Destroy(gameObject); // Destroy the enemy object
    }
}