using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;
    public Button[] buttons; 
    private int selectedIndex = 0; 
    private bool isCreditsOpen = false; // Track if the credits panel is open
    private int creditsButtonIndex = 0; // Track the index of the selected button in the credits panel
    public Button backButton; // Reference to the back button in the credits panel

    private void Start()
    {
        SelectButton();
    }

    private void Update()
    {
        // Check if credits panel is open
        if (isCreditsOpen)
        {
            // Check for back key (e.g., Escape)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideCredits();
            }

            // Check for up arrow key
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                creditsButtonIndex--;
                if (creditsButtonIndex < 0) creditsButtonIndex = 0; // Stay on the back button
                SelectCreditsButton();
            }

            // Check for down arrow key
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                creditsButtonIndex++;
                if (creditsButtonIndex > 0) creditsButtonIndex = 0; // Stay on the back button
                SelectCreditsButton();
            }

            // Check for enter key, but only if the back button is highlighted
            if (creditsButtonIndex == 0 && Input.GetKeyDown(KeyCode.Return))
            {
                backButton.onClick.Invoke(); // Simulate back button click
            }

            return; // Exit early if credits panel is open
        }

        // Main menu input handling
        // Check for up arrow key
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = buttons.Length - 1; 
            SelectButton();
        }

        // Check for down arrow key
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
            if (selectedIndex >= buttons.Length) selectedIndex = 0; 
            SelectButton();
        }

        // Check for enter key
        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedIndex].onClick.Invoke(); 
        }
    }

    private void SelectButton()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            ColorBlock colors = buttons[i].colors;
            colors.normalColor = (i == selectedIndex) ? Color.yellow : Color.white; 
            buttons[i].colors = colors;
        }
    }

    private void SelectCreditsButton()
    {
        // Highlight the back button in the credits panel
        ColorBlock colors = backButton.colors;
        colors.normalColor = (creditsButtonIndex == 0) ? Color.yellow : Color.white; // Highlight back button
        backButton.colors = colors;
    }

    public void StartGame()
    {
        // Uncomment the line below for build
        // SceneManager.LoadScene("MainScene");  
        Debug.Log("START!");
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);  
        isCreditsOpen = true; // Mark credits panel as open
        creditsButtonIndex = 0; // Reset credits button selection
        SelectCreditsButton(); // Highlight the back button
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);  
        isCreditsOpen = false; // Mark credits panel as closed
        selectedIndex = 0; // Reset selection to the first button when returning to the main menu
        SelectButton(); // Re-select main menu button
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();  
    }
}
