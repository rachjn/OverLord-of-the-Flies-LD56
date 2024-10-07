using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;
    public string sceneToLoad = "PlayerSelect"; // Name of the scene to load
    private bool isCreditsOpen = false; // To track if the credits panel is open

    AudioManager audioManager;

    protected virtual void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    

    private void Update()
    {
        // Check if the Escape key is pressed and if the credits panel is open
        if (Input.GetKeyDown(KeyCode.Escape) && isCreditsOpen)
        {
            audioManager.PlaySFX(audioManager.pickUp);
            HideCredits(); // Hide the credits panel when Escape is pressed
        }
    }

    // This function will be called when the Start button is clicked
    public void StartGame()
    {
        Debug.Log("START!");
        audioManager.PlaySFX(audioManager.metal);
        StartCoroutine(LoadSceneWithDelay(0.4f, sceneToLoad));

        // SceneManager.LoadScene(sceneToLoad);  // Load the next scene
    }

      // Coroutine to delay scene transition
    IEnumerator LoadSceneWithDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadScene(sceneName);      // Load the next scene
    }


    // This function will be called when the Credits button is clicked
    public void ShowCredits()
    {
        audioManager.PlaySFX(audioManager.pickUp);
        creditsPanel.SetActive(true);  // Show the credits panel
        isCreditsOpen = true; // Mark credits as open
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);  // Hide the credits panel
        isCreditsOpen = false; // Mark credits as closed
    }

    // This function will be called when the Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();  // Quit the application
    }
}
