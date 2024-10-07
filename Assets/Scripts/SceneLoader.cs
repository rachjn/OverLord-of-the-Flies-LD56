using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad = "MainScene"; // Name of the scene to load
    AudioManager audioManager;


 private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        // Detect Enter key press
        // Debug output for Player colors
        if (Input.GetKeyDown(KeyCode.Return)) // Enter key
        {
            audioManager.PlaySFX(audioManager.metal);
          
            
            StartCoroutine(LoadSceneWithDelay(0.1f, "MainScene"));
        }
    }

      IEnumerator LoadSceneWithDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadScene(sceneName);      // Load the next scene
    }

    private void OnMouseDown()
    {
        // Load scene when sprite is clicked
       audioManager.PlaySFX(audioManager.metal);
            
            StartCoroutine(LoadSceneWithDelay(0.1f, "MainScene"));
    }

    private void LoadNextScene()
    {
        // Load the next scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
