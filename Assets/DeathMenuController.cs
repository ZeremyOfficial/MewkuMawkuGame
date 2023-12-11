using UnityEngine;
using UnityEngine.UI;

public class DeathMenuController : MonoBehaviour
{
    private CanvasGroup canvasGroup; // Reference to the CanvasGroup component of the death menu.

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on the DeathMenuPanel game object.");
        }
        else
        {
            HideDeathMenu(); // Hide the death menu when the scene starts.
        }
    }

    public void ShowDeathMenu()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f; // Show the death menu by making it fully visible.
            canvasGroup.interactable = true; // Allow interaction with the death menu elements.
            canvasGroup.blocksRaycasts = true; // Allow raycasts to interact with the death menu elements.
        }
    }

    public void HideDeathMenu()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; // Hide the death menu by making it fully transparent.
            canvasGroup.interactable = false; // Disable interaction with the death menu elements.
            canvasGroup.blocksRaycasts = false; // Disable raycasts for the death menu elements.
        }
    }

    public void ResetDeathMenuState()
    {
        // Reset any necessary elements in the death menu here.
    }
}