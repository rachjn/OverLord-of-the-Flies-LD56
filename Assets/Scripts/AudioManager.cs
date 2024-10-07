using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("----- Audio Clip -----")]

    public AudioClip die;
    public AudioClip background;
    public AudioClip hit;
    public AudioClip pickUp;
    public AudioClip cheer;
    public AudioClip metal;

    public float sfxVolume = 1.0f; // Default volume (1.0 = max volume, 0.0 = min)

    private void Start()
    {
        // Set background music to loop
        musicSource.loop = true; // Enable looping

        // Set the background clip and play it
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }
}
