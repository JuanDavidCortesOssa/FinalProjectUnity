using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePause : MonoBehaviour
{
    [SerializeField] private GameObject panelPause;
    public bool paused;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            ChangePaused();
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
}
