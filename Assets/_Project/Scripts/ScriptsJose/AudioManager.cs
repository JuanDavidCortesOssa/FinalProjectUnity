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
    [SerializeField] private AudioClip moveSfx;
    [SerializeField] private AudioClip victorySfx;
    [SerializeField] private AudioClip attackSfx;
    [SerializeField] private AudioClip exclamationSfx;

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

    public void PlaySecondSound()
    {
        soundMusic.PlayOneShot(music);
    }
    
    [ContextMenu("PlayMove")]
    public void PlayMoveSfx()
    {
        soundSfx.PlayOneShot(moveSfx);
    }

    public void PlayVictorySfx()
    {
        soundSfx.PlayOneShot(victorySfx);
    }

    public void PlayAttackSfx()
    {
        soundSfx.PlayOneShot(attackSfx);
    }

    public void PlayExclamationSfx()
    {
        soundSfx.PlayOneShot(exclamationSfx);
    }
}
