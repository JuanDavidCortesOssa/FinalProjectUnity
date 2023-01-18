using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerJ : Singleton<UIManagerJ>
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private int maxTurns;
    private int stars;
    public void ActivateWinCanvas()
    {
        winScreen.SetActive(true);
    }

    public void ActivateLoseCanvas()
    {
        loseScreen.SetActive(true);
    }

    public void ActivateLoseCanvas(int amountOfTurns)
    {
        if (amountOfTurns > maxTurns)
        {
            stars = 2;
            if (amountOfTurns > (maxTurns+2))
            {
                stars = 1;
            }
        }
        else
        {
            stars = 3;
        }

        loseScreen.SetActive(true);
    }
}
