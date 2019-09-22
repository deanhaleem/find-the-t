using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public static float MainTimer;
    public static bool Lost;

    public Text ScoreText;
    public Text LivesText;
    public Text TimerText;
    public Text HighScore;
    public static int Score;
    public static int Lives = 3;

    void Start()
    {
        // set up Hud text
        ScoreText.text = "Score: " + Score;
        LivesText.text = "Lives: " + Lives;

        // set up timer
        if (GameController.DifficultyLevel == GameController.Difficulty.Easy)
        {
            MainTimer = 5;
            HighScore.text = "Easy High Score: " + PlayerPrefs.GetInt("Easy High Score: ");
        }
        else if (GameController.DifficultyLevel == GameController.Difficulty.Medium)
        {
            MainTimer = 4;
            HighScore.text = "Medium High Score: " + PlayerPrefs.GetInt("Medium High Score: ");
        }
        else
        {
            MainTimer = 3;
            HighScore.text = "Hard High Score: " + PlayerPrefs.GetInt("Hard High Score: ");
        }

        TimerText.text = "Time: " + MainTimer.ToString("n2");

        Lost = false;
    }

    void Update()
    {
        if (Lost)
        {
            return;
        }

        if (IndicatorController.IndicatorTimer < 0)
        {
            MainTimer -= Time.deltaTime;
            TimerText.text = "Time: " + MainTimer.ToString("n2");
        }

        if (MainTimer <= 0)
        {
            Lost = true;
        }

        if (Lives < 1)
        {
            if (GameController.DifficultyLevel == GameController.Difficulty.Easy)
            {
                if (Score > PlayerPrefs.GetInt("Easy High Score: ", 0))
                {
                    PlayerPrefs.SetInt("Easy High Score: ", Score);
                }
            }
            else if (GameController.DifficultyLevel == GameController.Difficulty.Medium)
            {
                if (Score > PlayerPrefs.GetInt("Medium High Score: ", 0))
                {
                    PlayerPrefs.SetInt("Medium High Score: ", Score);
                }
            }
            else
            {
                if (Score > PlayerPrefs.GetInt("Hard High Score: ", 0))
                {
                    PlayerPrefs.SetInt("Hard High Score: ", Score);
                }
            }
            Lives = 3;
            Score = 0;
            SceneManager.LoadScene("Menu");
        }
    }
}
