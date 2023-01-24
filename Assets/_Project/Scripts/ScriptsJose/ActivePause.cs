using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePause : MonoBehaviour
{
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelHelp;
    public bool paused;
    public bool help;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && help == false)
        {
            ChangePaused();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && help == true)
        {
            Help();
        }
    }

    public void ChangePaused()
    {
        if(!paused)
        {
            paused = true;
            panelPause.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            paused = false;
            panelPause.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void Help()
    {
        if (!help)
        {
            help = true;
            panelHelp.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            help = false;
            panelHelp.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
