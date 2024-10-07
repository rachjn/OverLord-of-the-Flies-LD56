using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if any key or button is pressed
        if (Input.anyKeyDown)
        {
            Debug.Log("Returning");
            // Load the home screen (assuming it's called "MainMenu")
            SceneManager.LoadScene("MenuScene");
        }
    }
}
