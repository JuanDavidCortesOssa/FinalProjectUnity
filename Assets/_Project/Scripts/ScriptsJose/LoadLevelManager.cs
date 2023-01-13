using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelManager : MonoBehaviour
{
    public static LoadLevelManager instance;

    private int gameMode;
    private int difficultyLevel = 0;
    public bool nextLevelLoad = false;
    private int levelLoad;

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
                Debug.Log("Modo normal");
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
                levelLoad = Random.Range(2, 4);
                Debug.Log(levelLoad);
                break;
            case 2:
                levelLoad = Random.Range(4, 6);
                Debug.Log(levelLoad);
                break;
            case 3:
                levelLoad = Random.Range(6, 8);
                Debug.Log(levelLoad);
                break;
            case 4:
                levelLoad = 8;
                Debug.Log(levelLoad);
                difficultyLevel = 0;
                break;
        }
    }
}
