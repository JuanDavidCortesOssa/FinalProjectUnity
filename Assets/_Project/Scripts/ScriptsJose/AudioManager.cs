using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private AudioSource sound;

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
        sound = GetComponent<AudioSource>();
    }

    public void PlaySecondSound()
    {
        sound.PlayOneShot(music);
    }
}
