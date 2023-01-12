using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //Add the different audiosource
    public AudioSource soundMusic;
    public AudioSource soundSfx;

    //Add the different sounds
    [SerializeField] private AudioClip music;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        soundMusic = GetComponent<AudioSource>();
        soundSfx = GetComponent<AudioSource>();
    }

    public void PlaySecondSound()
    {
        soundMusic.PlayOneShot(music);
    }
}
