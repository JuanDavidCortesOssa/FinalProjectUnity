using ConquistaGO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerJ : Singleton<UIManagerJ>
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Animator star1;
    [SerializeField] private Animator star2;
    [SerializeField] private Animator star3;
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

    [ContextMenu("Star1")]
    public void ActivateStar(int numberStars)
    {
        switch (numberStars)
        {
            case 1:
                star1.SetTrigger("Star1");
                break;
            case 2:
                star1.SetTrigger("Star1");
                Invoke("ActivateSecondStar", 0.5f);
                break;
            case 3:
                LevelsCompletedManager.instance.AddTemporallyStars();
                star1.SetTrigger("Star1");
                Invoke("ActivateSecondStar", 0.25f);
                Invoke("ActivateThirdStar", 0.5f);
                break;
        }
    }

    private void ActivateSecondStar()
    {
        star2.SetTrigger("Star2");
    }

    private void ActivateThirdStar()
    {
        star3.SetTrigger("Star3");
    }

    public void ActivateWinCanvas(int amountOfTurns)
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

        winScreen.SetActive(true);
        ActivateStar(stars);
    }
}
