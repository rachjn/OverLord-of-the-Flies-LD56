using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused = false;
    void Update()
    {
        // Toggle pause menu when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Hide the pause menu
        Time.timeScale = 1f;  // Resume normal game time
        isPaused = false;
    }

    // Pause the game
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Show the pause menu
        Time.timeScale = 0f;  // Freeze game time
        isPaused = true;
    }

    // Restart the current level
    public void RestartGame()
    {
        Debug.Log("Restarting Game...");
        Time.timeScale = 1f;  // Ensure normal game time before reloading
        SceneManager.LoadScene("MainScene");  // Reload the current scene
    }

    // Quit the game (works in a build, not in the editor)
    public void ReturnToMenu()
    {
        GameManager.Instance.winner = "none";
        Time.timeScale = 1f;  // Ensure normal game time before reloading
        Debug.Log("Returning to menu...");
        SceneManager.LoadScene("MenuScene");  // go to menu
    }
}
