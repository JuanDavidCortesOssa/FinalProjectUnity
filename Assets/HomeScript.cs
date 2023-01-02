using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScript : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;

    private void Start()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        startButton.onClick.AddListener(StartButtonFunc);
        optionsButton.onClick.AddListener(OptionsButtonFunc);
    }

    private void StartButtonFunc()
    {
        SceneManager.LoadScene("Level1");
    }

    private void OptionsButtonFunc()
    {
        //
    }
}
