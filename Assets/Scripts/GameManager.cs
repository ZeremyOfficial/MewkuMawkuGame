using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool fireballUnlocked;
    public int speedUpgradeCount; // Track speed upgrades

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return; // Exit the method to prevent further execution
        }

        // Initialize unlocked states and upgrade counts based on PlayerPrefs
        fireballUnlocked = PlayerPrefs.GetInt("FireballUnlocked", 0) == 1;
        speedUpgradeCount = PlayerPrefs.GetInt("SpeedUpgradeCount", 0); // Load the speed upgrade count
    }

    public void UnlockFireball()
    {
        fireballUnlocked = true;
        PlayerPrefs.SetInt("FireballUnlocked", 1);
        PlayerPrefs.Save();

        // Load the shop scene after unlocking the fireball
        SceneManager.LoadScene("ShopScene");
    }

    public void IncreaseSpeedUpgradeCount()
    {
        speedUpgradeCount++;
        PlayerPrefs.SetInt("SpeedUpgradeCount", speedUpgradeCount);
        PlayerPrefs.Save();
    }

    public bool FireballUnlocked()
    {
        return fireballUnlocked;
    }

    public void ApplySpeedUpgradesToPlayer(PlayerMovement playerMovement)
    {
        // This method assumes that the PlayerMovement script has a method
        // called UpgradeSpeed that takes a float parameter for the speed increase.
        // The PlayerMovement script must be in the active scene for this to work.
        float speedIncreaseAmount = 0.2f; // The amount speed increases per upgrade
        if (playerMovement != null)
        {
            playerMovement.UpgradeSpeed(speedIncreaseAmount * speedUpgradeCount);
        }
    }

    // Resets the purchases, upgrades, and score to their original values
    public void ResetPurchasesAndUpgrades()
    {
        fireballUnlocked = false;
        speedUpgradeCount = 0;
        PlayerPrefs.SetInt("FireballUnlocked", 0);
        PlayerPrefs.SetInt("SpeedUpgradeCount", 0);
        PlayerPrefs.Save();

        // Reset the score through the ScoreScript
        if (ScoreScript.instance != null)
        {
            ScoreScript.instance.ResetScore();
        }
    }
}
