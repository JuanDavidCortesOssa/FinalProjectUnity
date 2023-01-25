using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelMainUi : MonoBehaviour
{
    [SerializeField] private Button buttonContinue;
    [SerializeField] private GameObject warningNewGame;
    private int progressLevel;

    private void Start()
    {
        progressLevel = PlayerPrefs.GetInt("levelsCompleted");
        Debug.Log(progressLevel);
        if (progressLevel < 1)
        {
            buttonContinue.enabled = false;
        }
        else if (progressLevel > 12)
        {
            buttonContinue.enabled = false;
            LevelsCompletedManager.instance.RestartLevelsCompleted();
            progressLevel = 0;
        }
        else
        {
            buttonContinue.enabled = true;
        }
    }

    public void NewGame()
    {
        if (progressLevel < 1)
        {
            LevelsCompletedManager.instance.LoadNextLevel();
        }
        else
        {
            warningNewGame.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        LevelsCompletedManager.instance.LoadNextLevel();
    }

    public void RestartLevels()
    {
        LevelsCompletedManager.instance.RestartLevelsCompleted();
        LevelsCompletedManager.instance.LoadNextLevel();
    }
}
