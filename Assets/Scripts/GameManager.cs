using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public bool fireballUnlocked;

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
            return; // Exit the method to prevent further execution
        }

        // Load the total score (ScoreScript) if it exists
        GameObject scoreScriptObject = GameObject.Find("ScoreScript");
        if (scoreScriptObject != null)
        {
            ScoreScript scoreScript = scoreScriptObject.GetComponent<ScoreScript>();
            if (scoreScript != null)
            {
                scoreScript.LoadTotalScore();
            }
        }
    }
    
    public void UnlockFireball()
    {
        fireballUnlocked = true;
        PlayerPrefs.SetInt("FireballUnlocked", 1);
        PlayerPrefs.Save();
        
        // Load the shop scene after unlocking the fireball
        SceneManager.LoadScene("ShopScene");
    }
    
    // Rest of the GameManager code...
}