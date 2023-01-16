using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelManager : MonoBehaviour
{
    public static LoadLevelManager instance;

    private int gameMode;
    private int difficultyLevel = 0;
    public bool nextLevelLoad = false;
    private int levelLoad = 1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(nextLevelLoad)
        {
            nextLevelLoad = false;
            if(gameMode == 1)
            {
                difficultyLevel += 1;
                LoadNextLevelRandom();
            }
            else
            {
                LoadNextLevel();
            }
        }
    }

    public void GetGameMode()
    {
        gameMode = PlayerPrefs.GetInt("GameMode");
    }

    public void ActivateNextLevel()
    {
        nextLevelLoad = true;
    }

    private void LoadNextLevelRandom()
    {
        switch (difficultyLevel)
        {
            case 1:
                levelLoad = Random.Range(1, 3);
                SceneManager.LoadScene(levelLoad);
                break;
            case 2:
                levelLoad = Random.Range(3, 6);
                SceneManager.LoadScene(levelLoad);
                break;
            case 3:
                levelLoad = 6;
                SceneManager.LoadScene(levelLoad);
                difficultyLevel = 0;
                RestartLevel();
                break;
        }
    }

    public void RestartLevel()
    {
        levelLoad = 1;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelLoad);
        levelLoad++;
    }

    public void RestartGame()
    {
        levelLoad = 1;
        difficultyLevel = 0;
        nextLevelLoad = false;
    }
}
