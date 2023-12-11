using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;

    private int perRunScore;
    private int totalScore;
    private float survivalTime = 0;
    public TextMeshProUGUI perRunScoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        totalScore = PlayerPrefs.GetInt("PlayerScore", 0);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        ResetPerRunScore();
    }

    void Update()
    {
        survivalTime += Time.deltaTime;
        if (survivalTime >= 30f)
        {
            AddPoints(25);
            survivalTime = 0f;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Overworld")
        {
            GameObject runScoreObj = GameObject.FindGameObjectWithTag("RunScore");
            if (runScoreObj != null)
            {
                perRunScoreText = runScoreObj.GetComponent<TextMeshProUGUI>();
                UpdatePerRunScoreText();
            }
            ResetPerRunScore();
        }
        else
        {
            perRunScoreText = null;
        }
    }

    public void ShowTotalScoreInDeathMenu()
    {
        GameObject deathMenuPanel = GameObject.FindGameObjectWithTag("DeathMenu");
        if (deathMenuPanel != null)
        {
            TextMeshProUGUI deathMenuTotalScoreText = deathMenuPanel.GetComponentInChildren<TextMeshProUGUI>(true);
            if (deathMenuTotalScoreText != null)
            {
                deathMenuTotalScoreText.text = "Total Score: " + totalScore;
            }
        }
    }

    public void RetryButtonPressed()
    {
        ResetPerRunScore();
    }

    private void UpdatePerRunScoreText()
    {
        if (perRunScoreText != null)
        {
            perRunScoreText.text = "Score: " + perRunScore;
        }
    }

    public void AddPoints(int points)
    {
        perRunScore += points;
        UpdatePerRunScoreText();

        totalScore += points;
        SaveTotalScore();
    }

    public void SaveTotalScore()
    {
        PlayerPrefs.SetInt("PlayerScore", totalScore);
        PlayerPrefs.Save();
    }

    public void LoadTotalScore()
    {
        totalScore = PlayerPrefs.GetInt("PlayerScore", 0);
        UpdatePerRunScoreText(); // Ensure the UI is updated when the score is loaded
    }

    public void ResetPerRunScore()
    {
        perRunScore = 0;
        survivalTime = 0;
        UpdatePerRunScoreText();
    }

    public bool SpendScore(int amount)
    {
        if (totalScore >= amount)
        {
            totalScore -= amount;
            SaveTotalScore();
            return true;
        }
        return false;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public void ResetScore()
    {
        totalScore = 0;
        SaveTotalScore();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
