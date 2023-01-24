using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeControllerMusic;
    public Slider volumeControllerSfx;
    public GameObject audioMusic;
    public GameObject audioSfx;

    private void Start()
    {
        audioMusic = GameObject.FindGameObjectWithTag("AudioMusic");
        audioSfx = GameObject.FindGameObjectWithTag("AudioSfx");
        volumeControllerMusic.value = PlayerPrefs.GetFloat("volumeMusic", 0.5f);
        volumeControllerSfx.value = PlayerPrefs.GetFloat("volumeSfx", 1f);
    }

    private void Update()
    {
        audioMusic.GetComponent<AudioSource>().volume = volumeControllerMusic.value;
        audioSfx.GetComponent<AudioSource>().volume = volumeControllerSfx.value;
    }

    public void SaveVolumeMusic()
    {
        PlayerPrefs.SetFloat("volumeMusic", volumeControllerMusic.value);
    }
    
    public void SaveVolumeSfx()
    {
        PlayerPrefs.SetFloat("volumeSfx", volumeControllerSfx.value);
    }
}
