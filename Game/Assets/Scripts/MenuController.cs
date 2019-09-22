using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private BannerView bannerView;

    private static bool adsLoaded = false;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            return;
        }

        string appId = "ca-app-pub-9067871168279980~7246076221";

        if (!adsLoaded)
        {
            MobileAds.Initialize(appId);
            adsLoaded = true;
        }

        RequestBanner();
        DisplayBanner();
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-9067871168279980/2912079547";


        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void DisplayBanner()
    {
        bannerView.Show();
    }

    public void PlayEasyGame()
    {
        GameController.DifficultyLevel = GameController.Difficulty.Easy;
        IndicatorController.IndicatorTimer = 1;
        bannerView.Destroy();
        SceneManager.LoadScene("Game");
    }

    public void PlayMediumGame()
    {
        GameController.DifficultyLevel = GameController.Difficulty.Medium;
        IndicatorController.IndicatorTimer = 1;
        bannerView.Destroy();
        SceneManager.LoadScene("Game");
    }

    public void PlayHardGame()
    {
        GameController.DifficultyLevel = GameController.Difficulty.Hard;
        IndicatorController.IndicatorTimer = 1;
        bannerView.Destroy();
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        if (GameController.DifficultyLevel == GameController.Difficulty.Easy)
        {
            if (HudController.Score > PlayerPrefs.GetInt("Easy High Score: ", 0))
            {
                PlayerPrefs.SetInt("Easy High Score: ", HudController.Score);
            }
        }
        else if (GameController.DifficultyLevel == GameController.Difficulty.Medium)
        {
            if (HudController.Score > PlayerPrefs.GetInt("Medium High Score: ", 0))
            {
                PlayerPrefs.SetInt("Medium High Score: ", HudController.Score);
            }
        }
        else
        {
            if (HudController.Score > PlayerPrefs.GetInt("Hard High Score: ", 0))
            {
                PlayerPrefs.SetInt("Hard High Score: ", HudController.Score);
            }
        }
        HudController.Lives = 3;
        HudController.Score = 0;
        SceneManager.LoadScene("Menu");
    }

    public void QuitApp()
    {
        if (GameController.DifficultyLevel == GameController.Difficulty.Easy)
        {
            if (HudController.Score > PlayerPrefs.GetInt("Easy High Score: ", 0))
            {
                PlayerPrefs.SetInt("Easy High Score: ", HudController.Score);
            }
        }
        else if (GameController.DifficultyLevel == GameController.Difficulty.Medium)
        {
            if (HudController.Score > PlayerPrefs.GetInt("Medium High Score: ", 0))
            {
                PlayerPrefs.SetInt("Medium High Score: ", HudController.Score);
            }
        }
        else
        {
            if (HudController.Score > PlayerPrefs.GetInt("Hard High Score: ", 0))
            {
                PlayerPrefs.SetInt("Hard High Score: ", HudController.Score);
            }
        }
        Application.Quit();
    }
}
