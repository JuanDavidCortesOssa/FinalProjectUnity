using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeSkinMainUI : MonoBehaviour
{
    private int skinsUnlocked;
    private int skinNumber;
    [SerializeField] private GameObject skinLock2;
    [SerializeField] private GameObject skinUnlock2;
    [SerializeField] private GameObject skinLock3;
    [SerializeField] private GameObject skinUnlock3;
    [SerializeField] private GameObject skinSelect1;
    [SerializeField] private GameObject imgCheck1;
    [SerializeField] private GameObject skinSelect2;
    [SerializeField] private GameObject imgCheck2;
    [SerializeField] private GameObject skinSelect3;
    [SerializeField] private GameObject imgCheck3;
    [SerializeField] private GameObject textObject2;
    [SerializeField] private GameObject textObject3;
    [SerializeField] private TMP_Text skinText2;
    [SerializeField] private TMP_Text skinText3;
    private int levelsClompleted;

    private void Start()
    {
        skinsUnlocked = PlayerPrefs.GetInt("skinsUnlocked", 1);
        skinNumber = PlayerPrefs.GetInt("skinNumber", 1);
        levelsClompleted = PlayerPrefs.GetInt("levelsCompleted");
        LoadSkinsUnlocked(skinsUnlocked);
        ChangeSkinMain(skinNumber);
        LoadSkinSelected(skinNumber);
    }

    private void LoadSkinsUnlocked(int number)
    {
        switch (number)
        {
            case 1:
                skinLock2.SetActive(true);
                skinUnlock2.SetActive(false);
                skinLock3.SetActive(true);
                skinUnlock3.SetActive(false);
                skinSelect2.SetActive(false);
                skinSelect3.SetActive(false);
                textObject2.SetActive(true);
                textObject3.SetActive(true);
                LoadTextSkin(2);
                break;
            case 2:
                skinLock2.SetActive(false);
                skinUnlock2.SetActive(true);
                skinLock3.SetActive(true);
                skinUnlock3.SetActive(false);
                skinSelect2.SetActive(true);
                skinSelect3.SetActive(false);
                textObject2.SetActive(false);
                textObject3.SetActive(true);
                LoadTextSkin(1);
                break;
            case 3:
                skinLock2.SetActive(false);
                skinUnlock2.SetActive(true);
                skinLock3.SetActive(false);
                skinUnlock3.SetActive(true);
                skinSelect2.SetActive(true);
                skinSelect3.SetActive(true);
                textObject2.SetActive(false);
                textObject3.SetActive(false);
                break;
        }
    }

    private void LoadTextSkin(int textNumber)
    {
        switch(textNumber)
        {
            case 1:
                skinText3.text = levelsClompleted.ToString();
                break;
            case 2:
                skinText2.text = levelsClompleted.ToString();
                skinText3.text = levelsClompleted.ToString();
                break;
        }
    }

    public void ChangeSkinMain(int skinSelected)
    {
        PlayerPrefs.SetInt("skinNumber", skinSelected);
        LoadSkinSelected(skinSelected);
    }

    public void LoadSkinSelected(int skin)
    {
        switch(skin)
        {
            case 1:
                skinSelect1.GetComponent<Button>().enabled = false;
                imgCheck1.SetActive(true);
                skinSelect2.GetComponent<Button>().enabled = true;
                imgCheck2.SetActive(false);
                skinSelect3.GetComponent<Button>().enabled = true;
                imgCheck3.SetActive(false);
                break;
            case 2:
                skinSelect1.GetComponent<Button>().enabled = true;
                imgCheck1.SetActive(false);
                skinSelect2.GetComponent<Button>().enabled = false;
                imgCheck2.SetActive(true);
                skinSelect3.GetComponent<Button>().enabled = true;
                imgCheck3.SetActive(false);
                break;
            case 3:
                skinSelect1.GetComponent<Button>().enabled = true;
                imgCheck1.SetActive(false);
                skinSelect2.GetComponent<Button>().enabled = true;
                imgCheck2.SetActive(false);
                skinSelect3.GetComponent<Button>().enabled = false;
                imgCheck3.SetActive(true);
                break;
        }
    }
}
