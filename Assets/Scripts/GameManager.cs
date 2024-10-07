using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton class for controlling the game.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Color Player1Color
    {
        get {return GameManager.Instance.player1Color;}
        set {GameManager.Instance.player1Color = value;}
    }
    public Color Player2Color
    {
        get {return GameManager.Instance.player2Color;}
        set {GameManager.Instance.player2Color = value;}
    }
    public string winner;
    private Color player1Color;
    private Color player2Color;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("I'm killing myself");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Player1Color = new Color(0.1f, 0.1f, 0.9f);
        Player2Color = new Color(0.9f, 0.1f, 0.1f);

        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("");
    }

    public void WinGame(string player)
    {
        SceneManager.LoadScene("");
    }
}
