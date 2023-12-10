using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    private int perRunScore; // Score for the current run
    private int totalScore; // Total accumulated score
    private float survivalTime = 0; // Initialize survivalTime to zero
    public TextMeshProUGUI perRunScoreText; // Reference to the per-run score text
    public TextMeshProUGUI totalScoreText; // Reference to the total score text

    void Awake()
    {
        // Load the total accumulated score from PlayerPrefs at the very start
        LoadTotalScore();
    }

    void Start()
    {
        // Reset the per-run score to 0 every time a new game starts
        ResetPerRunScore();
    }

    void Update()
    {
        // Regularly update the per-run score during the game
        survivalTime += Time.deltaTime;
        if (survivalTime >= 30f)
        {
            AddPoints(25);
            survivalTime = 0f;
        }
    }

    private void UpdatePerRunScoreText()
    {
        // Update the displayed per-run score text during the game
        if (perRunScoreText != null)
            perRunScoreText.text = "Score: " + perRunScore;
    }

    private void UpdateTotalScoreText()
    {
        // Update the displayed total score text with the loaded score
        if (totalScoreText != null)
            totalScoreText.text = "Total Score: " + totalScore;
    }

    public void AddPoints(int points)
    {
        // Increase the per-run score
        perRunScore += points;
        UpdatePerRunScoreText();

        // Update the total score immediately
        totalScore += points;
        UpdateTotalScoreText();

        // Save the updated total score
        SaveTotalScore();
    }

    public void SaveTotalScore()
    {
        // Save the total accumulated score to PlayerPrefs
        PlayerPrefs.SetInt("PlayerScore", totalScore);
        PlayerPrefs.Save();
    }

    public void LoadTotalScore()
    {
        // Load the total accumulated score from PlayerPrefs
        totalScore = PlayerPrefs.GetInt("PlayerScore", 0);
        UpdateTotalScoreText();
    }

    public void ResetPerRunScore()
    {
        // Reset the per-run score to zero
        perRunScore = 0;
        survivalTime = 0; // Also reset survival time to avoid immediate point increase
        UpdatePerRunScoreText();
    }

    public bool SpendScore(int amount)
    {
        // Deduct the spent amount from the total score and save it
        int currentTotalScore = totalScore;
        if (currentTotalScore >= amount)
        {
            PlayerPrefs.SetInt("PlayerScore", currentTotalScore - amount);
            PlayerPrefs.Save();

            // Update the displayed total score text
            LoadTotalScore();
            return true;
        }
        return false;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}
