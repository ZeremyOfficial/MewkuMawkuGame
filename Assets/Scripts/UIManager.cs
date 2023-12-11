using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject deathMenuPanel;
    public TimerScript timerScript; // This should be set if TimerScript is in the same scene as UIManager

    private void Start()
    {
        // Hide the death menu at the start
        HideDeathMenu();

        // Start the timer if it's in the same scene
        if (timerScript != null)
        {
            timerScript.StartTimer();
        }
    }

    public void ShowDeathMenu()
    {
        if (deathMenuPanel != null)
        {
            deathMenuPanel.SetActive(true);
            Time.timeScale = 0;

            // Use the instance of ScoreScript to update the total score in the death menu
            if (ScoreScript.instance != null)
            {
                ScoreScript.instance.ShowTotalScoreInDeathMenu();
            }
        }
        else
        {
            Debug.LogError("DeathMenuPanel reference not assigned in the Inspector.");
        }
    }

    public void HideDeathMenu()
    {
        if (deathMenuPanel != null)
        {
            deathMenuPanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Debug.LogWarning("DeathMenuPanel reference not assigned in the Inspector, but trying to hide it.");
        }
    }

    public void Retry()
    {
        // Use the instance of ScoreScript to reset the per-run score
        if (ScoreScript.instance != null)
        {
            ScoreScript.instance.ResetPerRunScore();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Restart the timer if it's in the same scene
        if (timerScript != null)
        {
            timerScript.StartTimer();
        }

        HideDeathMenu();
    }

    public void OpenShop()
    {
        HideDeathMenu();
        SceneManager.LoadScene("ShopScene");
    }

    public void BackToMenu()
    {
        HideDeathMenu();
        SceneManager.LoadScene("MainMenu");
    }
}
