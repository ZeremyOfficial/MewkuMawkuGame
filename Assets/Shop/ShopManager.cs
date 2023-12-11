using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI fireballButtonText;
    public TextMeshProUGUI speedUpgradeButtonText;
    public TextMeshProUGUI totalScoreText;
    public Button purchaseButton;
    public Button speedUpgradeButton;
    public Button mainMenuButton;
    public Button resetButton;

    private int fireballCost = 1250;
    private int speedUpgradeCost;

    void Start()
    {
        speedUpgradeCost = GameManager.instance.speedUpgradeCost;
        UpdateTotalScoreDisplay();
        CheckFireballPurchaseStatus();
        UpdateSpeedUpgradeButton();

        // Add onClick listeners for the buttons
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        resetButton.onClick.AddListener(ResetPurchasesAndUpgrades);
    }

    public void CheckFireballPurchaseStatus()
    {
        if (GameManager.instance.fireballUnlocked)
        {
            fireballButtonText.text = "Thank you for your patronage";
            purchaseButton.interactable = false;
        }
        else
        {
            fireballButtonText.text = "Buy Fireball (Cost: " + fireballCost + ")";
            purchaseButton.interactable = true;
        }
    }

    public void PurchaseFireball()
    {
        if (ScoreScript.instance.GetTotalScore() >= fireballCost)
        {
            ScoreScript.instance.SpendScore(fireballCost);
            GameManager.instance.UnlockFireball();
            fireballButtonText.text = "Thank you for your patronage";
            purchaseButton.interactable = false;
        }
        else
        {
            StartCoroutine(ShowNotEnoughScoreMessage(fireballButtonText, "Buy Fireball (Cost: " + fireballCost + ")"));
        }
        UpdateTotalScoreDisplay();
    }

    public void PurchaseSpeedUpgrade()
    {
        if (ScoreScript.instance.GetTotalScore() >= speedUpgradeCost)
        {
            ScoreScript.instance.SpendScore(speedUpgradeCost);
            GameManager.instance.IncreaseSpeedUpgradeCount();
            speedUpgradeCost *= 2;
            GameManager.instance.UpdateSpeedUpgradeCost(speedUpgradeCost);
            StartCoroutine(UpdateSpeedUpgradeButtonAfterDelay("Speed Up!", 1f));
        }
        else
        {
            StartCoroutine(ShowNotEnoughScoreMessage(speedUpgradeButtonText, "Upgrade Speed (Cost: " + speedUpgradeCost + ")"));
        }
        UpdateTotalScoreDisplay();
    }

    public void ResetPurchasesAndUpgrades()
    {
        GameManager.instance.ResetPurchasesAndUpgrades();
        ScoreScript.instance.ResetTotalScore();
        speedUpgradeCost = 250;
        GameManager.instance.UpdateSpeedUpgradeCost(speedUpgradeCost);
        UpdateSpeedUpgradeButton();
        CheckFireballPurchaseStatus();
        UpdateTotalScoreDisplay();
    }

    public void LoadOverworldScene()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator UpdateSpeedUpgradeButtonAfterDelay(string message, float delay)
    {
        speedUpgradeButtonText.text = message;
        yield return new WaitForSeconds(delay);
        UpdateSpeedUpgradeButton();
    }

    private void UpdateSpeedUpgradeButton()
    {
        speedUpgradeButtonText.text = "Upgrade Speed (Cost: " + speedUpgradeCost + ")";
    }

    private IEnumerator ShowNotEnoughScoreMessage(TextMeshProUGUI buttonText, string originalText)
    {
        buttonText.text = "Not enough score";
        yield return new WaitForSeconds(2);
        buttonText.text = originalText;
    }

    private void UpdateTotalScoreDisplay()
    {
        totalScoreText.text = "Total Score: " + ScoreScript.instance.GetTotalScore();
    }
}
