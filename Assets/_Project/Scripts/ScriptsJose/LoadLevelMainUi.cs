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
        if (progressLevel < 1)
        {
            buttonContinue.enabled = false;
        }
        else
        {
            buttonContinue.enabled = true;
        }
    }

    public void NewGame()
    {
        if (progressLevel < 1 || progressLevel > 12)
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
