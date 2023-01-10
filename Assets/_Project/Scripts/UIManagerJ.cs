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

    public void ToggleCanvas()
    {
        UICanvas.SetActive(!UICanvas.activeSelf);
    }

    public void ActivateWinCanvas()
    {
        UICanvas.SetActive(true);
        winScreen.SetActive(true);
    }

    public void ActivateLoseCanvas()
    {
        UICanvas.SetActive(true);
        loseScreen.SetActive(true);
    }
}
