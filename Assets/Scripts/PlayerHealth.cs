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
    private Coroutine invincibilityCoroutine;

    // Public property to access the invincibility status
    public bool IsInvincible
    {
        get { return isInvincible; }
        private set { isInvincible = value; }
    }

    void Start()
    {
        currentHealth = maxHealth;
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
                if (invincibilityCoroutine != null)
                {
                    StopCoroutine(invincibilityCoroutine); // Stop the current running coroutine if any
                }
                invincibilityCoroutine = StartCoroutine(BecomeTemporarilyInvincible());
            }
        }
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        float originalSpeed = playerMovement.Speed; // Get the current speed
        playerMovement.Speed *= speedBoostFactor; // Apply speed boost

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        playerMovement.Speed = originalSpeed; // Reset speed to original
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        
        // Stop enemy spawning
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.ToggleSpawning(false);
        }

        // Destroy the player GameObject
        Destroy(gameObject);

        // Show the death menu
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.ShowDeathMenu();
        }

        // Additional death handling logic here

        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine); // Ensure no coroutine is left running
            playerMovement.Speed = playerMovement.originalSpeed; // Reset speed to original if needed
        }
    }
}
	