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
        get {
            Debug.Log(GameManager.Instance.player1Color);
            return GameManager.Instance.player1Color;
            }
        set {
            Debug.Log(value);
            GameManager.Instance.player1Color = value;
            }
    }
    public Color Player2Color
    {
        get {return GameManager.Instance.player2Color;}
        set {GameManager.Instance.player2Color = value;}
    }
    public string winner = "";
    private Color player1Color = new Color(0.1f, 0.1f, 0.9f);
    private Color player2Color = new Color(0.9f, 0.1f, 0.1f);

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("I'm killing myself");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
        winner = "";
    }

    public void WinGame(string player)
    {
        if (winner == "")
        {
            winner = player;
            SceneManager.sceneLoaded += OnWinScreen;
            SceneManager.LoadScene("EndScene");
        }
    }

    void OnWinScreen(Scene _, LoadSceneMode __)
    {
        var winDisplay1 = GameObject.Find("fly1win");
        var winDisplay2 = GameObject.Find("fly2win");
        var paperSprite = GameObject.Find("long paper").GetComponent<SpriteRenderer>();
        if (winDisplay1 != null && winDisplay2 != null)
        {
            if (winner == "Player1")
            {
                paperSprite.color = player1Color;
                winDisplay1.SetActive(false);
                winDisplay2.SetActive(true);
            }
            else 
            {
                paperSprite.color = player2Color;
                winDisplay1.SetActive(true);
                winDisplay2.SetActive(false);
            }
        }
        SceneManager.sceneLoaded -= OnWinScreen;
    }
}
