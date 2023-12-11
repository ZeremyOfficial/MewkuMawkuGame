using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;

    private int perRunScore;
    private int totalScore;
    private int highScore;
    private float survivalTime = 0;
    public TextMeshProUGUI perRunScoreText;
    private bool isInOverworld = false;

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
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        ResetPerRunScore();
    }

    void Update()
    {
        if (isInOverworld)
        {
            survivalTime += Time.deltaTime;
            if (survivalTime >= 30f)
            {
                AddPoints(25);
                survivalTime = 0f;
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isInOverworld = scene.name == "Overworld";

        if (isInOverworld)
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
            TextMeshProUGUI[] texts = deathMenuPanel.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (var text in texts)
            {
                if (text.gameObject.name == "TotalScoreText")
                {
                    text.text = "Total Score: " + totalScore;
                }
                else if (text.gameObject.name == "HighScoreText")
                {
                    text.text = "High Score: " + highScore;
                }
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

        if (perRunScore > highScore)
        {
            highScore = perRunScore;
            SaveHighScore();
        }
    }

    public void SaveTotalScore()
    {
        PlayerPrefs.SetInt("PlayerScore", totalScore);
        PlayerPrefs.Save();
    }

    public void LoadTotalScore()
    {
        totalScore = PlayerPrefs.GetInt("PlayerScore", 0);
        UpdatePerRunScoreText();
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

    // Resets only the total score, not the high score.
    public void ResetTotalScore()
    {
        totalScore = 0;
        SaveTotalScore();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public int GetHighScore()
    {
        return highScore;
    }
}
