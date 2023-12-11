using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI totalScoreText; // Add a reference to the Total Score TextMeshProUGUI

    public int fireballCost = 1250;
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene by its name
    }

    public void RetryOverworld()
    {
        // Make sure GameManager and ScoreScript instances exist
        if (GameManager.instance != null && ScoreScript.instance != null)
        {
            // Reset the per-run score
            ScoreScript.instance.ResetPerRunScore();

            // Set the time scale to 1 (unfreeze the game)
            Time.timeScale = 1;

            // Get the name of the Overworld scene
            string overworldSceneName = "Overworld";

            // Check if the Overworld scene is currently loaded
            if (SceneManager.GetSceneByName(overworldSceneName).isLoaded)
            {
                // The Overworld scene is already loaded, so just restart it
                SceneManager.LoadScene(overworldSceneName);
            }
            else
            {
                // The Overworld scene is not loaded, so load it
                SceneManager.LoadScene(overworldSceneName, LoadSceneMode.Single);
            }
        }
        else
        {
            // Handle the case where instances are not initialized
            Debug.LogError("ScoreScript or GameManager is not initialized!");
        }
    }


    // Ensure that the GameManager and ScoreScript instances are initialized
    private void Awake()
    {
        // Make sure GameManager exists and is initialized
        if (GameManager.instance == null)
        {
            // Handle the absence of GameManager here, e.g., load it from a prefab
            Debug.LogError("GameManager is missing or not initialized!");
        }

        // Make sure ScoreScript exists and is initialized
        if (ScoreScript.instance == null)
        {
            // Handle the absence of ScoreScript here, e.g., load it from a prefab
            Debug.LogError("ScoreScript is missing or not initialized!");
        }
    }

    // Update the Total Score display
    private void UpdateTotalScoreDisplay()
    {
        if (totalScoreText != null && ScoreScript.instance != null)
        {
            totalScoreText.text = "Total Score: " + ScoreScript.instance.GetTotalScore();
        }
    }

    private void Start()
    {
        // Update the Total Score display when the shop scene starts
        UpdateTotalScoreDisplay();
    }

    public void PurchaseFireball()
    {
        if (ScoreScript.instance != null && GameManager.instance != null)
        {
            if (ScoreScript.instance.GetTotalScore() >= fireballCost)
            {
                ScoreScript.instance.SpendScore(fireballCost);
                GameManager.instance.UnlockFireball();
                buttonText.text = "Thank you for your patronage";

                // Update the Total Score display after the purchase
                UpdateTotalScoreDisplay();
            }
            else
            {
                StartCoroutine(ShowNotEnoughScoreMessage());
            }
        }
        else
        {
            // Handle the case where instances are not initialized
            Debug.LogError("ScoreScript or GameManager is not initialized!");
        }
    }

    private IEnumerator ShowNotEnoughScoreMessage()
    {
        string originalText = buttonText.text;
        buttonText.text = "Not enough score";
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        buttonText.text = originalText;
    }
}
