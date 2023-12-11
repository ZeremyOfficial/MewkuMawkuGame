using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private float survivalTime = 0f;
    public TextMeshProUGUI timerText;
    private bool isTimerActive = false;

    void Start()
    {
        UpdateTimerDisplay(); // Initialize the timer display on start
    }

    void Update()
    {
        if (isTimerActive)
        {
            survivalTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StartTimer()
    {
        isTimerActive = true;
        survivalTime = 0f;
        UpdateTimerDisplay();
    }

    public void StopTimer()
    {
        isTimerActive = false;
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(survivalTime);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}