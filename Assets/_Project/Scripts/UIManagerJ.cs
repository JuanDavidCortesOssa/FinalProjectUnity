using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerJ : Singleton<UIManagerJ>
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public void ActivateWinCanvas()
    {
        winScreen.SetActive(true);
    }

    public void ActivateLoseCanvas()
    {
        loseScreen.SetActive(true);
    }
}
