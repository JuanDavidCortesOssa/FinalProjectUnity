using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsCompletedManager : MonoBehaviour
{
    private int levelCompleted;
    private int tempStars;
    public bool levelStars = false;

    public static LevelsCompletedManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
        DontDestroyOnLoad(gameObject);
        levelCompleted = PlayerPrefs.GetInt("levelsCompleted", 0);
        tempStars = PlayerPrefs.GetInt("numberStars", 0);
        Debug.Log(tempStars);
    }

    public void LoadNextLevel()
    {
        levelStars = false;
        NextLevel(levelCompleted);
    }

    private void NextLevel(int numberLevel)
    {
        switch(numberLevel)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene(2);
                break;
            case 2:
                SceneManager.LoadScene(3);
                break;
            case 3:
                SceneManager.LoadScene(4);
                break;
            case 4:
                SceneManager.LoadScene(5);
                break;
            case 5:
                SceneManager.LoadScene(6);
                break;
            case 6:
                SceneManager.LoadScene(8);
                break;
            case 7:
                SceneManager.LoadScene(9);
                break;
            case 8:
                SceneManager.LoadScene(10);
                break;
            case 9:
                SceneManager.LoadScene(11);
                break;
            case 10:
                SceneManager.LoadScene(12);
                break;
            case 11:
                SceneManager.LoadScene(13);
                break;
            case 12:
                PlayerPrefs.SetInt("skinsUnlocked", 3);
                SceneManager.LoadScene(14);
                PlayerPrefs.SetInt("numberStars", tempStars);
                break;
            case 13:
                SceneManager.LoadScene(0);
                break;
        }
    }

    public void SaveLevelCompleted()
    {
        levelCompleted++;
        PlayerPrefs.SetInt("levelsCompleted", levelCompleted);
    }

    public void RestartLevelsCompleted()
    {
        PlayerPrefs.SetInt("levelsCompleted", 0);
        PlayerPrefs.SetInt("numberStars", 0);
        tempStars = 0;
        levelStars = false;
        levelCompleted = 0;
    }

    public void AddTemporallyStars()
    {
        tempStars = tempStars + 3;
        levelStars = true;
        Debug.Log(tempStars);
    }

    public void RemoveTempStars()
    {
        tempStars = tempStars - 3;
        levelStars = false;
    }

    public void SaveStars()
    {
        levelStars = false;
        PlayerPrefs.SetInt("numberStars", tempStars);
        Debug.Log(tempStars);
    }
}
