using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI buttonText; // Text for the fireball purchase button
    public TextMeshProUGUI totalScoreText;
    public Button purchaseButton; // Fireball purchase button
    public Button speedUpgradeButton; // Speed upgrade button
    public Button mainMenuButton; // Main Menu button
    public Button resetButton; // Reset purchases and upgrades button

    public int fireballCost = 1250;
    private int speedUpgradeCost = 250;

    void Start()
    {
        UpdateTotalScoreDisplay();
        CheckFireballPurchaseStatus();
        UpdateSpeedUpgradeButton(); // Initialize the speed upgrade button text
    }

    public void CheckFireballPurchaseStatus()
    {
        if (GameManager.instance.fireballUnlocked)
        {
            buttonText.text = "Thank you for your patronage";
            purchaseButton.interactable = false;
        }
        else
        {
            buttonText.text = "Buy Fireball (Cost: " + fireballCost + ")";
            purchaseButton.interactable = true;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryOverworld()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void PurchaseSpeedUpgrade()
    {
        // Check if we have enough score to purchase the upgrade
        if (ScoreScript.instance.GetTotalScore() >= speedUpgradeCost)
        {
            // Spend the score and increase the upgrade count in the GameManager
            ScoreScript.instance.SpendScore(speedUpgradeCost);
            GameManager.instance.IncreaseSpeedUpgradeCount(); // Call GameManager to increment the speed upgrade count

            // Update the cost for the next upgrade and the UI
            speedUpgradeCost *= 2;
            StartCoroutine(UpdateSpeedUpgradeButtonAfterDelay("Speed Up!", 1f)); // Show "Speed Up!" for 1 second
            UpdateTotalScoreDisplay();
        }
        else
        {
            StartCoroutine(ShowNotEnoughScoreMessage());
        }
    }

    public void ResetPurchasesAndUpgrades()
    {
        GameManager.instance.ResetPurchasesAndUpgrades(); // Reset the upgrades in GameManager
        speedUpgradeCost = 250; // Reset speed upgrade cost
        UpdateSpeedUpgradeButton(); // Update the speed upgrade button text
        CheckFireballPurchaseStatus(); // Update the fireball purchase button
        UpdateTotalScoreDisplay(); // Update the total score display
    }

    private IEnumerator UpdateSpeedUpgradeButtonAfterDelay(string message, float delay)
    {
        UpdateSpeedUpgradeButtonText(message);
        yield return new WaitForSeconds(delay);
        UpdateSpeedUpgradeButtonText("Upgrade Speed (Cost: " + speedUpgradeCost + ")");
    }

    private void UpdateSpeedUpgradeButtonText(string text)
    {
        TextMeshProUGUI speedUpgradeText = speedUpgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        if (speedUpgradeText != null)
        {
            speedUpgradeText.text = text;
        }
    }

    private void UpdateSpeedUpgradeButton()
    {
        UpdateSpeedUpgradeButtonText("Upgrade Speed (Cost: " + speedUpgradeCost + ")");
    }

    private IEnumerator ShowNotEnoughScoreMessage()
    {
        string originalText = buttonText.text;
        buttonText.text = "Not enough score";
        yield return new WaitForSeconds(2);
        buttonText.text = originalText;
    }

    private void UpdateTotalScoreDisplay()
    {
        if (totalScoreText != null && ScoreScript.instance != null)
        {
            totalScoreText.text = "Total Score: " + ScoreScript.instance.GetTotalScore();
        }
    }
}
