using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider volumeController;
    public GameObject aud;

    private void Start()
    {
        aud = GameObject.FindGameObjectWithTag("audio");
        volumeController.value = PlayerPrefs.GetFloat("volumeSave", 1f);
    }

    private void Update()
    {
        aud.GetComponent<AudioSource>().volume = volumeController.value;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("volumeSave", volumeController.value);
    }
}
