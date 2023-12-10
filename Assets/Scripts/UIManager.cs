using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject deathMenuPanel;
    public TextMeshProUGUI totalScoreText; // Reference to the TextMeshProUGUI that will display the total score in the death menu
    public ScoreScript scoreScript;

    public void ShowDeathMenu()
    {
        deathMenuPanel.SetActive(true);
        Time.timeScale = 0; // Freeze the game

        // Display the total accumulated score on the death menu
        if (totalScoreText != null && scoreScript != null)
        {
            totalScoreText.text = "Total Score: " + scoreScript.GetTotalScore();
        }
    }

    public void Retry()
    {
        Time.timeScale = 1; // Unfreeze the game

        if (scoreScript != null)
        {
            scoreScript.ResetPerRunScore(); // Reset the per-run score
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void OpenShop()
    {
        // Logic to open the shop panel
    }

    public void BackToMenu()
    {
        Time.timeScale = 1; // Unfreeze the game
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    // Additional methods as needed
}