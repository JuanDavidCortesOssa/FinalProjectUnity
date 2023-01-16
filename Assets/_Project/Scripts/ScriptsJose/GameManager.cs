using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Toggle toggle;
    private int gameMode;

    private void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

    public void SetGameMode(int gameMode)
    {
        PlayerPrefs.SetInt("GameMode", gameMode);
        LoadLevelManager.instance.RestartGame();
        LoadLevelManager.instance.GetGameMode();
        LoadLevelManager.instance.ActivateNextLevel();
    }

    public void NextLevel()
    {
        LoadLevelManager.instance.ActivateNextLevel();
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void RestartAll()
    {
        LoadLevelManager.instance.RestartGame();
        LoadLevelManager.instance.ActivateNextLevel();
    }
}
