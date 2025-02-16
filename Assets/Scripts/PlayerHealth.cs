using UnityEngine;
using System.Collections; // Add this line

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float invincibilityDurationSeconds = 2f;
    private bool isInvincible = false;
    private PlayerMovement playerMovement;
    private UIManager uiManager;
    private Coroutine invincibilityCoroutine;
    private TimerScript timerScript;

    public bool IsInvincible
    {
        get { return isInvincible; }
        private set { isInvincible = value; }
    }

    void Start()
    {
        currentHealth = maxHealth;
        playerMovement = GetComponentInParent<PlayerMovement>();
        uiManager = FindObjectOfType<UIManager>();
        timerScript = FindObjectOfType<TimerScript>(); 

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found on the player.");
        }
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene.");
        }
        if (timerScript == null)
        {
            Debug.LogError("TimerScript not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !isInvincible)
        {
            PlayerTakeDamage(10);
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
                    StopCoroutine(invincibilityCoroutine);
                }
                invincibilityCoroutine = StartCoroutine(BecomeTemporarilyInvincible());
            }
        }
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        isInvincible = false;
    }

    private void Die()
    {
        
        if (timerScript != null)
        {
            timerScript.StopTimer();
        }

        DisablePlayerActions();
        uiManager.ShowDeathMenu();
        Destroy(playerMovement.gameObject); 
    }

    private void DisablePlayerActions()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            var swordAttack = playerMovement.GetComponentInChildren<SwordAttack>();
            if (swordAttack != null)
            {
                swordAttack.enabled = false;
            }
        }

        Renderer[] renderers = playerMovement.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        Animator animator = playerMovement.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }
    }
}
