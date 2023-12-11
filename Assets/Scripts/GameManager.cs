using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool fireballUnlocked;
    public int speedUpgradeCount;
    public int speedUpgradeCost;

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
            return;
        }

        fireballUnlocked = PlayerPrefs.GetInt("FireballUnlocked", 0) == 1;
        speedUpgradeCount = PlayerPrefs.GetInt("SpeedUpgradeCount", 0);
        speedUpgradeCost = PlayerPrefs.GetInt("SpeedUpgradeCost", 250);
    }

    public void UnlockFireball()
    {
        fireballUnlocked = true;
        PlayerPrefs.SetInt("FireballUnlocked", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("ShopScene");
    }

    public void IncreaseSpeedUpgradeCount()
    {
        speedUpgradeCount++;
        PlayerPrefs.SetInt("SpeedUpgradeCount", speedUpgradeCount);
        PlayerPrefs.Save();
    }

    public void UpdateSpeedUpgradeCost(int newCost)
    {
        speedUpgradeCost = newCost;
        PlayerPrefs.SetInt("SpeedUpgradeCost", speedUpgradeCost);
        PlayerPrefs.Save();
    }

    public void ResetPurchasesAndUpgrades()
    {
        fireballUnlocked = false;
        speedUpgradeCount = 0;
        PlayerPrefs.SetInt("FireballUnlocked", 0);
        PlayerPrefs.SetInt("SpeedUpgradeCount", 0);
        PlayerPrefs.SetInt("SpeedUpgradeCost", 250);
        PlayerPrefs.Save();

        if (ScoreScript.instance != null)
        {
            ScoreScript.instance.ResetScore();
        }
    }
}