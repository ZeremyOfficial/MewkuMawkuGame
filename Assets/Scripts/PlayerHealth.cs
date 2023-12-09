using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float invincibilityDurationSeconds = 2f;
    public float speedBoostFactor = 1.5f;
    private bool isInvincible = false;
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script

    // Public property to access the invincibility status
    public bool IsInvincible
    {
        get { return isInvincible; }
        private set { isInvincible = value; }
    }

    void Start()
    {
        currentHealth = maxHealth;

        // Find the PlayerMovement script in the scene
        playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !isInvincible)
        {
            PlayerTakeDamage(10); // Apply damage
            StartCoroutine(BecomeTemporarilyInvincible()); // Start invincibility coroutine
        }
    }

    public void PlayerTakeDamage(int damageAmount)
    {
        if (!isInvincible)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(BecomeTemporarilyInvincible());
            }
        }
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        float originalSpeed = playerMovement.Speed; // Get the current speed
        playerMovement.Speed *= speedBoostFactor; // Apply speed boost

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        isInvincible = false;
        playerMovement.Speed = originalSpeed; // Reset speed to original
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Add death handling logic here
    }
}