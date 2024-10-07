using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCheer : MonoBehaviour
{
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        float cheerVolume = 0.5f; // Example volume (50% of max)
        audioManager.PlaySFX(audioManager.cheer, cheerVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
