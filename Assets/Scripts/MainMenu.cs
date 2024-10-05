using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;

    // This function will be called when the Start button is clicked
    public void StartGame()
    {
        // SceneManager.LoadScene("MainScene");  // uncomment for build
        Debug.Log("START!");
    }

    // This function will be called when the Credits button is clicked
    public void ShowCredits()
    {
        creditsPanel.SetActive(true);  // Show the credits panel
    }
    public void HideCredits()
    {
        creditsPanel.SetActive(false);  // Hide the credits panel
    }

    // This function will be called when the Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();  // This will work in a built game, not in the editor
    }
}
