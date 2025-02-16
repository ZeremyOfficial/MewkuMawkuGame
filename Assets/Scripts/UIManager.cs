    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class UIManager : MonoBehaviour
    {
        public GameObject deathMenuPanel;
        public TimerScript timerScript; 

        private void Start()
        {
            
            HideDeathMenu();

           
            if (timerScript != null)
            {
                timerScript.StartTimer();
            }
        }

        public void ShowDeathMenu()
        {
            if (deathMenuPanel != null)
            {
                deathMenuPanel.SetActive(true);
                Time.timeScale = 0;

             
                if (ScoreScript.instance != null)
                {
                    ScoreScript.instance.ShowTotalScoreInDeathMenu();
                }
            }
            else
            {
                Debug.LogError("DeathMenuPanel reference not assigned in the Inspector.");
            }
        }

        public void HideDeathMenu()
        {
            if (deathMenuPanel != null)
            {
                deathMenuPanel.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                Debug.LogWarning("DeathMenuPanel reference not assigned in the Inspector, but trying to hide it.");
            }
        }

        public void Retry()
        {
            
            if (ScoreScript.instance != null)
            {
                ScoreScript.instance.ResetPerRunScore();
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

           
            if (timerScript != null)
            {
                timerScript.StartTimer();
            }

            HideDeathMenu();
        }

        public void OpenShop()
        {
            HideDeathMenu();
            SceneManager.LoadScene("ShopScene");
        }

        public void BackToMenu()
        {
            HideDeathMenu();
            SceneManager.LoadScene("MainMenu");
        }
    }
