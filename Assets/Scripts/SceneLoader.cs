using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad = "MainScene"; // Name of the scene to load

    private void Update()
    {
        // Detect Enter key press
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextScene(); // Load scene when Enter is pressed
        }
    }

    private void OnMouseDown()
    {
        // Load scene when sprite is clicked
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        // Load the next scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
