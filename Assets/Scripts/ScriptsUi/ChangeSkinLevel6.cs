using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSkinLevel6 : MonoBehaviour
{
    [SerializeField] private GameObject skinUnlock1;
    [SerializeField] private GameObject skinSelect1;
    [SerializeField] private GameObject imgCheck1;
    [SerializeField] private GameObject skinUnlock2;
    [SerializeField] private GameObject skinSelect2;
    [SerializeField] private GameObject imgCheck2;
    [SerializeField] private Animator camAnimation;
    [SerializeField] private Animator houseAnimation;

    private void Start()
    {
        PlayerPrefs.SetInt("skinsUnlocked", 2);
        LoadSkinSelected(1);
        Invoke("AnimationsMidLevel", 0.5f);
    }

    public void LoadSkinSelected(int skin)
    {
        switch (skin)
        {
            case 1:
                skinSelect1.GetComponent<Button>().enabled = false;
                imgCheck1.SetActive(true);
                skinSelect2.GetComponent<Button>().enabled = true;
                imgCheck2.SetActive(false);
                break;
            case 2:
                skinSelect1.GetComponent<Button>().enabled = true;
                imgCheck1.SetActive(false);
                skinSelect2.GetComponent<Button>().enabled = false;
                imgCheck2.SetActive(true);
                break;
        }
    }

    private void AnimationsMidLevel()
    {
        camAnimation.SetTrigger("NewSkin");
        houseAnimation.SetTrigger("NewSkin");
    }

    public void ChangeSkinLevel(int skinSelected)
    {
        PlayerPrefs.SetInt("skinNumber", skinSelected);
        LoadSkinSelected(skinSelected);
    }

    public void Continue()
    {
        LevelsCompletedManager.instance.LoadNextLevel();
    }
}
