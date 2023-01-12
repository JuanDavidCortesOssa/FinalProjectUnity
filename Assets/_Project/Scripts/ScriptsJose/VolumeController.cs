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
        //aud = GameObject.FindGameObjectWithTag("audio");
        audioMusic = GameObject.FindGameObjectWithTag("AudioMusic");
        //audioMusic = AudioManager.instance.soundMusic.GetComponent<AudioSource>();
        audioSfx = GameObject.FindGameObjectWithTag("AudioSfx");
        //audioSfx = AudioManager.instance.soundSfx.GetComponent<AudioSource>();
        volumeControllerMusic.value = PlayerPrefs.GetFloat("volumeMusic", 1f);
        volumeControllerSfx.value = PlayerPrefs.GetFloat("volumeSfx", 1f);
    }

    private void Update()
    {
        audioMusic.GetComponent<AudioSource>().volume = volumeControllerMusic.value;
        audioSfx.GetComponent<AudioSource>().volume = volumeControllerSfx.value;
        /*audioMusic.volume = volumeControllerMusic.value;
        audioSfx.volume = volumeControllerSfx.value;*/
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
