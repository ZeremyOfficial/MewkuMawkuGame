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

        public int fireballCost = 1250;
        private int speedUpgradeCost = 250;

        void Start()
        {
            UpdateTotalScoreDisplay();
            CheckFireballPurchaseStatus();
            UpdateSpeedUpgradeButton();
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
                GameManager.instance.UnlockFireball(); // Assuming you have a method to unlock fireball
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
            speedUpgradeCost = 250;
            UpdateSpeedUpgradeButton();
            CheckFireballPurchaseStatus();
            UpdateTotalScoreDisplay();
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
        public void LoadOverworldScene()
        {
            SceneManager.LoadScene("Overworld");
        }

    }
