using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerJ : Singleton<UIManagerJ>
{
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;

    private void Start()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        homeButton.onClick.AddListener(HomeButtonFunc);
        restartButton.onClick.AddListener(RestartButtonFunc);
        nextLevelButton.onClick.AddListener(NextLevelButtonFunc);
    }

    public void ToggleCanvas()
    {
        UICanvas.SetActive(!UICanvas.activeSelf);
    }

    public void ActivateWinCanvas()
    {
        ToggleCanvas();
        winScreen.SetActive(true);
    }

    public void ActivateLoseCanvas()
    {
        ToggleCanvas();
        loseScreen.SetActive(true);
    }

    //Buttons functionalities
    private void HomeButtonFunc()
    {
        SceneManager.LoadScene("Home");
    }

    private void RestartButtonFunc()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevelButtonFunc()
    {
        //Needs to be implemented
    }
}
