using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayButtonClicked()
    {
        // Load the game scene
        SceneManager.LoadScene("Overworld");
    }
    public void ExitButtonClicked()
    {
        // Quit the game
        Application.Quit();
    }
}