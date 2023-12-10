using UnityEngine;
using TMPro; // Use the TextMesh Pro namespace

public class TimerScript : MonoBehaviour
{
    private float survivalTime = 0f; // The player's survival time in seconds
    public TextMeshProUGUI timerText; // Reference to the Timer TMP UI Text

    void Update()
    {
        // Increment the survival time
        survivalTime += Time.deltaTime;

        // Update the UI text with the survival time
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if (timerText != null) // Check if the TMP text component is assigned
        {
            int minutes = Mathf.FloorToInt(survivalTime / 60f);
            int seconds = Mathf.FloorToInt(survivalTime % 60f);
            string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);

            // Update the TMP UI text
            timerText.text = "Time: " + timerString;
        }
    }

    public void ResetTimer()
    {
        survivalTime = 0f; // Reset the survival time
        UpdateTimerText(); // Update the timer display
    }
}