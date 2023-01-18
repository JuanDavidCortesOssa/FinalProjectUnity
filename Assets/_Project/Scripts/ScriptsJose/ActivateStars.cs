using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ActivateStars : MonoBehaviour
{
    [SerializeField] private Animator star1;
    [SerializeField] private Animator star2;
    [SerializeField] private Animator star3;
    private int numberStars = 3;

    private void Awake()
    {
        star1.GetComponent<Animator>();
        star2.GetComponent<Animator>();
        star3.GetComponent<Animator>();
    }

    [ContextMenu("Star1")]
    public void ActivateStar()
    {
        switch(numberStars)
        {
            case 1:
                star1.SetTrigger("Star1");
                break;
            case 2:
                star1.SetTrigger("Star1");
                Invoke("ActivateSecondStar", 1.0f);
                break;
            case 3:
                star1.SetTrigger("Star1");
                Invoke("ActivateSecondStar", 1.0f);
                Invoke("ActivateThirdStar", 2.0f);
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
}
