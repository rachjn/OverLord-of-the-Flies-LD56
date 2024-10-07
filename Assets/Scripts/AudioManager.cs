using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("----- Audio Clip -----")]

// public bool playMusicOnStart = true; // Flag to control whether music plays on Start
    public AudioClip die;
    public AudioClip background;
    public AudioClip hit;
    public AudioClip pickUp;
    public AudioClip cheer;
    public AudioClip metal;
     public bool playMusicOnStart = true; 
    //  public bool playMusicOnStart = true; 

    public float sfxVolume = 1.0f; // Default volume (1.0 = max volume, 0.0 = min)


    private void Awake()
    {
        if (!playMusicOnStart)
        {
            musicSource.loop = true;
            musicSource.clip = background;
            musicSource.Play();
        }
    }
    private void Start()
    {
        // Only play music if the flag is true
        if (playMusicOnStart)
        {
            musicSource.loop = true;
            musicSource.clip = background;
            musicSource.Play();
        }
    }


    public void PlayMusic()
    {
        // Method to manually play music, for example, in the Menu scene
        if (!musicSource.isPlaying)
        {
            musicSource.loop = true;
            musicSource.clip = background;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }
}
