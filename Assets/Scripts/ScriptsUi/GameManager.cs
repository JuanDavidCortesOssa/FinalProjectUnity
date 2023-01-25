using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Toggle toggle;
    private int gameMode;
    private int skinsUnlocked;

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

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void RestartScene()
    {
        if(LevelsCompletedManager.instance.levelStars == true)
        {
            LevelsCompletedManager.instance.RemoveTempStars();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
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

    public void NextLevel()
    {
        LevelsCompletedManager.instance.SaveLevelCompleted();
        LevelsCompletedManager.instance.LoadNextLevel();
    }

    public void ExitLevelCompleted()
    {
        LevelsCompletedManager.instance.SaveLevelCompleted();
        LevelsCompletedManager.instance.SaveStars();
        SceneManager.LoadScene(0);
    }

    public void LoadSkinMidLevel()
    {
        skinsUnlocked = PlayerPrefs.GetInt("skinsUnlocked", 1);
        if(skinsUnlocked == 1)
        {
            LevelsCompletedManager.instance.SaveLevelCompleted();
            SceneManager.LoadScene(7);
        }
        else
        {
            NextLevel();
        }
    }

    public void RestartAllGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
