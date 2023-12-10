using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    private int score; // Score for the current run
    private int totalScore; // Total accumulated score
    private float survivalTime = 0; // Initialize survivalTime to zero
    public TextMeshProUGUI scoreText; // Reference to the Timer TMP UI Text
    public TextMeshProUGUI totalScoreText; // Reference to the total score text

    private bool isNewGameSession = true; // Flag to track if it's a new game session

    void Awake()
    {
        // Load the total accumulated score from PlayerPrefs at the very start
        LoadTotalScore();
    }

    void Start()
    {
        if (isNewGameSession)
        {
            // Reset the score for the current run to 0 every time a new game session starts
            ResetGameScore();
        }
    }

    void Update()
    {
        // Regularly update the score during the game
        survivalTime += Time.deltaTime;
        if (survivalTime >= 30f)
        {
            AddPoints(25);
            survivalTime = 0f;
        }
    }

    private void UpdateScoreText()
    {
        // Update the displayed score text during the game
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    private void UpdateTotalScoreText()
    {
        // Update the displayed total score text with the loaded score
        if (totalScoreText != null)
            totalScoreText.text = "Total Score: " + totalScore;
    }

    public void AddPoints(int points)
    {
        // Increase the score for the current run
        score += points;
        UpdateScoreText();

        // Update the total score immediately
        totalScore += points;
        UpdateTotalScoreText();
    }

    // Remove SaveScore method

    public void LoadTotalScore()
    {
        // Load the total accumulated score from PlayerPrefs
        totalScore = PlayerPrefs.GetInt("PlayerScore", 0);
        UpdateTotalScoreText();
    }

    // Keep GetTotalScore as it is

    public void ResetGameScore()
    {
        // Reset the score for the current run to zero
        score = 0;
        survivalTime = 0; // Also reset survival time to avoid immediate point increase
        UpdateScoreText();
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

    public void ResetDisplayedScore()
    {
        // Reset the displayed score to zero when starting a new game session
        ResetGameScore();
        isNewGameSession = false; // Clear the flag to indicate the continuation of the current session
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}
