using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayButtonClicked()
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

            // Load the Overworld scene in LoadSceneMode.Single to ensure it's the only active scene
            SceneManager.LoadScene(overworldSceneName, LoadSceneMode.Single);
        }
        else
        {
            // Handle the case where instances are not initialized
            Debug.LogError("ScoreScript or GameManager is not initialized!");
        }
    }

    public void ShopButtonClicked()
    {
        // Load the shop scene
        SceneManager.LoadScene("ShopScene");
    }

    public void ExitButtonClicked()
    {
        // Quit the game
        Application.Quit();
    }
}